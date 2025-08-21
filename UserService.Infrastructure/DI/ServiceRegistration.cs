using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Repositories;
using UserService.Application.Services;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UserDb")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService.Infrastructure.Services.UserService>();
            return services;
        }
    }
}
