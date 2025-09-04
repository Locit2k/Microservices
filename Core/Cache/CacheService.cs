using Core.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Core.Cache
{
    public class CacheService : ICacheService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public CacheService(HttpClient httpClient, IOptions<CacheOptions> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/get/{key}");
            var value = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(value))
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync(string key, string value)
        {
            var payload = new { key, value };
            await _httpClient.PostAsJsonAsync($"{_baseUrl}", payload);
        }

        public async Task RemoveAsync(string key)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{key}");
        }

    }
}
