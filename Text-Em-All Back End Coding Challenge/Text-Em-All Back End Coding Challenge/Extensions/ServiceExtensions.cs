using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Database;
using WebApp.DataAccess;

namespace WebApp.Extensions
{
  public static class ServiceExtensions
  {
    public static void ConfigureCors(this IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
      });
    }
    public static void ConfigureIISIntegration(this IServiceCollection services)
    {
      services.Configure<IISOptions>(options =>
      {

      });
    }
    public static void ConfigureLoggerService(this IServiceCollection services)
    {
      services.AddSingleton<ILoggerManager, LoggerManager>();
    }
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
    {
      var connectionString = config["sqlconnection:connectionString"];
      services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(connectionString));
    }
    public static void ConfigureRepositoryWrapper(this IServiceCollection services)
    {
      services.AddScoped<IRepository, Repository>();
    }
  }
}
