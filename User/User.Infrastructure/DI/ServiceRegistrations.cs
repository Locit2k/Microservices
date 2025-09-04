using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Services;
using User.Infrastructure.Persistence;
using User.Infrastructure.Repositories;

namespace User.Infrastructure.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            // Register other infrastructure services here if needed
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Connection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, User.Infrastructure.Services.UserService>();
            return services;
        }
    }
}
