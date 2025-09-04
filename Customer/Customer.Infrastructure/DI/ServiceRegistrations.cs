using Core.Repositories;
using Customer.Application.Services;
using Customer.Infrastructure.Persistence;
using Customer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Infrastructure.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            // Register other infrastructure services here if needed
            services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Connection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerService, Services.CustomerService>();
            return services;
        }
    }
}
