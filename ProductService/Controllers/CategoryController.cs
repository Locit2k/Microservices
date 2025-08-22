using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Models;
using ProductService.Application.Services;
using ProductService.Domain.Enities;

namespace ProductService.API.Controllers
{
    [Route("api/category/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Check()
        {
            return Ok("Category api check");
        }

        [HttpPost]
        public async Task<DataResponse<Categories>> Add(Categories data)
        {
            return await _categoryService.AddAsync(data);
        }

        [HttpPut]
        public async Task<DataResponse<Categories>> Update(Categories data)
        {
            return await _categoryService.UpdateAsync(data);
        }

        [HttpDelete]
        public async Task<DataResponse<bool>> Delete(string categoryID)
        {
            return await _categoryService.DeleteAsync(categoryID);
        }
    }
}
