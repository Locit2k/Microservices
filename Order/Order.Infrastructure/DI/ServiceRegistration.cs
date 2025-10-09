
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Events;
using Order.Application.Services;
using Order.Infrastructure.Events;
using Order.Infrastructure.Persistence;
using Order.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, Services.OrderService>();
            services.AddSingleton<IEventbus, RabbitMqPublisher>();
            //services.AddHostedService<RabbitMqConsumer>();
            return services;
        }
    }
}
