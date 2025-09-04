
using RedisCache.API.Interfaces;
using RedisCache.API.Services;
using Serilog;
using StackExchange.Redis;

namespace RedisCache.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, services, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration.GetSection("Logging"))
            );
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = builder.Configuration.GetSection("Redis")["ConnectionString"];
                return ConnectionMultiplexer.Connect(configuration);
            });
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
