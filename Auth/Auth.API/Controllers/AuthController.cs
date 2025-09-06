using Auth.Application.DTOs;
using Auth.Application.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/auths/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Check()
        {
            return Ok("Auth service checked");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<object>> Login([FromBody] LoginDTO data)
        {
            return await _authService.Login(data);
        }

        [HttpPost]
        public async Task<DataResponse<bool>> Logout([FromQuery] string username)
        {
            return await _authService.Logout(username);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<bool>> Register([FromBody] RegisterDTO data)
        {
            return await _authService.Register(data);
        }
    }
}
