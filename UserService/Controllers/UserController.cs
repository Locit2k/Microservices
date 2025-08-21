using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Models;
using UserService.Application.Services;

namespace UserService.API.Controllers
{
    [Route("api/user/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Check()
        {
            return Ok("User api check");
        }

        [HttpPost]
        public async Task<DataResponse<UserDTO>> Add(UserDTO data)
        {
            return await _userService.AddAsync(data);
        }

        [HttpPut]
        public async Task<DataResponse<UserDTO>> Update(UserDTO data)
        {
            return await _userService.UpdateAsync(data);
        }

        [HttpDelete]
        public async Task<DataResponse<bool>> Delete(string userID)
        {
            return await _userService.DeleteAsync(userID);
        }
    }
}
