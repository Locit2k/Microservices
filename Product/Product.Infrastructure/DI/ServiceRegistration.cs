using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Events;
using Product.Application.Services;
using Product.Infrastructure.Events;
using Product.Infrastructure.Persistence;
using Product.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, Services.CategoryService>();
            services.AddScoped<IProductService, Services.ProductService>();
            services.AddScoped<IEventbus, RabbitMqPublisher>();
            services.AddHostedService<RabbitMqConsumer>();
            return services;
        }
    }
}
