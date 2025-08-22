using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs;
using ProductService.Application.Models;
using ProductService.Application.Services;

namespace ProductService.Controllers
{
    [Route("api/product/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Check()
        {
            return Ok("Product api check");
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

        [HttpDelete]
        public async Task<DataResponse<bool>> Delete(Guid productID)
        {
            return await _productService.DeleteAsync(productID);
        }
    }
}
