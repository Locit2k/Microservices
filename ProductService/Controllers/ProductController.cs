using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [Route("api/product/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult Check()
        {
            return Ok("Product api check");
        }
    }
}
