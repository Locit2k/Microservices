using Auth.Application.DTOs;
using Auth.Application.Services;
using Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.API.Controllers
{
    [Route("api/auths")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService, ILogger<AuthsController> logger)
        {
            _authService = authService;
        }

        [HttpGet("check")]
        [AllowAnonymous]
        public IActionResult Check()
        {
            return Ok("Auth service checked");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<DataResponse<object>> Login([FromBody] LoginDTO data)
        {
            return await _authService.Login(data);
        }

        [HttpPost("logout")]
        public async Task<DataResponse<bool>> Logout([FromQuery] string username)
        {
            return await _authService.Logout(username);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<DataResponse<bool>> Register([FromBody] RegisterDTO data)
        {
            return await _authService.Register(data);
        }

        //[HttpGet("google-login")]
        //[AllowAnonymous]
        //public IActionResult GoogleLogin([FromQuery] string username)
        //{
        //    var properties = new AuthenticationProperties
        //    {
        //        RedirectUri = "/api/auths/google-callback"
        //    };
        //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        //}

        //[HttpGet("google-callback")]
        //[AllowAnonymous]
        //public async Task<DataResponse<object>> GoogleCallback()
        //{
        //    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    if (!result.Succeeded || result?.Principal == null)
        //    {
        //        return new DataResponse<object>(StatusCodes.Status400BadRequest, "Xác thực Google không thành công.");
        //    }
        //    var email = result.Principal.FindFirstValue(ClaimTypes.Email);
        //    return new DataResponse<object>(StatusCodes.Status200OK, "Liên kết google thành công!");
        //}
    }
}
