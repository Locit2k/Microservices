using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/order/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check()
        {
            return Ok("Order api check");
        }
    }
}
