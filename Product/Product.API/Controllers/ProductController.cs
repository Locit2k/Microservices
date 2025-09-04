using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Application.DTOs;
using Product.Application.Services;

namespace Product.API.Controllers
{
    [Route("api/products/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Check()
        {
            return Ok("Product service checked");
        }

        [HttpPost]
        public async Task<DataResponse<ProductDTO>> Add(ProductDTO data)
        {
            return await _productService.AddAsync(data);
        }

        [HttpPut]
        public async Task<DataResponse<ProductDTO>> Update(ProductDTO data)
        {
            return await _productService.UpdateAsync(data);
        }

        [HttpDelete("{recID}")]
        public async Task<DataResponse<bool>> Delete(Guid recID)
        {
            return await _productService.DeleteAsync(recID);
        }
    }
}
