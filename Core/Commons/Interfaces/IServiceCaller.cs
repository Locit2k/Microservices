using Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Commons.Interfaces
{
    public interface IServiceCaller
    {
        Task<T?> CallApiAsync<T>(HttpMethod httpMethod, string serviceName, string methodName, params object[] data);
    }
}
