using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [Route("api/user/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IActionResult GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
