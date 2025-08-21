using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Mappings;
using UserService.Application.Validations;

namespace UserService.Application.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<UserDTOValidations>();
            services.AddAutoMapper(typeof(UserMappingProfile));
            return services;
        }
    }
}
