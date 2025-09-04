using Core.Cache;
using Core.Models;
using Product.Application.DI;
using Product.Infrastructure.DI;
using Serilog;
namespace Product.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration.GetSection("Logging")));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection("Cache"));
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ICacheService, CacheService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
