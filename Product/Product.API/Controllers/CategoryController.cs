using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Services;
using Product.Domain.Entities;

namespace Product.API.Controllers
{
    [Route("api/categories/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Check()
        {
            return Ok("Category service checked");
        }

        [HttpPost]
        public async Task<DataResponse<Categories>> Add([FromBody] Categories data)
        {
            return await _categoryService.AddAsync(data);
        }

        [HttpPut]
        public async Task<DataResponse<Categories>> Update([FromBody] Categories data)
        {
            return await _categoryService.UpdateAsync(data);
        }

        [HttpDelete("{recID}")]
        public async Task<DataResponse<bool>> Delete(Guid recID)
        {
            return await _categoryService.DeleteAsync(recID);
        }
    }
}
