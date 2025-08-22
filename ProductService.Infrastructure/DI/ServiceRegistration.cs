using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Repositories;
using ProductService.Application.Services;
using ProductService.Infrastructure.Persistence;
using ProductService.Infrastructure.Repositories;
using ProductService.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ProductDb")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService.Infrastructure.Services.ProductService>();
            return services;
        }
    }
}
