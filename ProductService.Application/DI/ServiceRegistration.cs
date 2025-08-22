using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Mappings;
using ProductService.Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.DI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductMappingProfile));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<ProductValidations>();
            return services;
        }
    }
}
