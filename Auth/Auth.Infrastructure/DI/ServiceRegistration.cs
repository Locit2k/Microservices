
using Auth.Application.Services;
using Core.Commons.Interfaces;
using Core.Commons.Services;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, Services.AuthService>();
            services.AddScoped<IServiceCaller, ServiceCaller>();
            services.AddHttpClient();
            return services;
        }
    }
}
