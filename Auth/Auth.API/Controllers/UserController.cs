using Auth.Application.DTOs;
using Auth.Application.Services;
using Core.Cache;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Auth.API.Controllers
{
    [Route("api/users/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICacheService _cache;
        public UserController(IUserService userService, ICacheService cache)
        {
            _userService = userService;
            _cache = cache;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Check()
        {
            return Ok("User service checked");
        }

        [HttpPost]
        public async Task<DataResponse<UserDTO>> Add([FromBody] UserDTO data)
        {
            return await _userService.AddAsync(data);
        }

        [HttpPut]
        public async Task<DataResponse<UserDTO>> Update([FromBody] UserDTO data)
        {
            return await _userService.UpdateAsync(data);
        }

        [HttpDelete("{userID}")]
        public async Task<DataResponse<bool>> Delete(string userID)
        {
            return await _userService.DeleteAsync(userID);
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> SetCache(string key, [FromBody] UserDTO data)
        {
            await _cache.SetAsync(key, JsonSerializer.Serialize(data));
            return Ok("Set cache thành công");
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCache(string key)
        {

            var value = await _cache.GetAsync<UserDTO>(key);
            return Ok(value);
        }
    }
}
