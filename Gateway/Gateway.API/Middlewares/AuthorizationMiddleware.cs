using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Gateway.API.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtOptions _jwtOptions;
        private readonly JwtInternalOptions _jwtInternalOptions;

        public AuthorizationMiddleware(RequestDelegate next, IOptions<JwtOptions> jwtOptions, IOptions<JwtInternalOptions> jwtInternalOptions)
        {
            _next = next;
            _jwtOptions = jwtOptions.Value;
            _jwtInternalOptions = jwtInternalOptions.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var endpoint = context.GetEndpoint();
                if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
                {
                    await _next(context);
                    return;
                }
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var principal = ValidateToken(token);
                if (principal == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid token.");
                    return;
                }

                var internalToken = GenerateInternalToken(principal);
                context.Request.Headers["Authorization"] = $"Bearer {internalToken}";
                await _next(context);
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token.");
            }
        }

        private ClaimsPrincipal? ValidateToken(string? token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);

            return tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true
            }, out SecurityToken validatedToken);
        }

        private string GenerateInternalToken(ClaimsPrincipal principal)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtInternalOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, principal.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""),
                new Claim(ClaimTypes.Role, principal.FindFirstValue(ClaimTypes.Role) ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: _jwtInternalOptions.Issuer,
                audience: _jwtInternalOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtInternalOptions.Expiration),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
