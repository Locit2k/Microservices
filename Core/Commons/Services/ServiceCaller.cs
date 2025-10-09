using Core.Commons.Interfaces;
using Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Commons.Services
{
    public class ServiceCaller : IServiceCaller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ServiceCaller(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<T?> CallApiAsync<T>(HttpMethod httpMethod, string serviceName, string methodName, params object[] data)
        {
            var baseUrl = _configuration[$"Services:{serviceName}"];
            var request = new HttpRequestMessage(httpMethod, $"{baseUrl}/{methodName}");
            if (data != null && (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put))
            {
                request.Content = JsonContent.Create(data);
            }
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var dataResponse = await response.Content.ReadFromJsonAsync<DataResponse<T>>();
            return dataResponse != null ? dataResponse.Data : default;
        }
    }
}
