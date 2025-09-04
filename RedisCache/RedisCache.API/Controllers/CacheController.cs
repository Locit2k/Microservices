using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedisCache.API.Interfaces;

namespace RedisCache.API.Controllers
{
    [Route("api/cache/[action]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Check()
        {
            return Ok("Cache service checked");
        }

        [HttpPost("{key}")]
        public async Task Set(string key, [FromBody] object item)
        {
            await _cacheService.SetAsync(key, item, TimeSpan.FromMinutes(30));
        }

        [HttpGet("{key}")]
        public async Task<object?> Get(string key)
        {
            return await _cacheService.GetAsync<object>(key);
        }

        [HttpDelete("{key}")]
        public async Task<bool> Remove(string key)
        {
            return await _cacheService.RemoveAsync(key);
        }
    }
}
