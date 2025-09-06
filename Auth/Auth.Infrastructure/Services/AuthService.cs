using Auth.Application.DTOs;
using Auth.Application.Services;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly JwtOptions _jwtOptions;
        public AuthService(ILogger<AuthService> logger, IOptions<JwtOptions> jwtOptions)
        {
            _logger = logger;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<DataResponse<object>> Login(LoginDTO data)
        {
            try
            {
                return new DataResponse<object>(StatusCodes.Status200OK, "Đăng nhập thành công!", data = null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login error: {JsonSerializer.Serialize(ex)}");
                return new DataResponse<object>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Đăng nhập không thành công, vui lòng thử lại.",
                    Error = new ErrorDetails
                    {
                        Code = "INTERNAL_SERVER_ERROR",
                        Message = ex.Message
                    }
                };
            }
        }

        public Task<DataResponse<bool>> Logout(string username)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<bool>> Register(RegisterDTO data)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(string userID, string roleID)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userID),
                    new Claim(ClaimTypes.Role, roleID)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpiration),
                    signingCredentials: creds
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GenerateToken error: {JsonSerializer.Serialize(ex)}.");
                return string.Empty;
            }
        }

        public string HashPassword(object user, string password)
        {
            var _hasher = new PasswordHasher<object>();
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(object user, string hashedPassword, string inputPassword)
        {
            var _hasher = new PasswordHasher<object>();
            var verificationResult = _hasher.VerifyHashedPassword(user, hashedPassword, inputPassword);
            return verificationResult == PasswordVerificationResult.Success;
        }
    }
}
