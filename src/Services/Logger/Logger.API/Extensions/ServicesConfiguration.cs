using Logger.API.Contracts;
using Logger.API.Options;
using Logger.API.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logger.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMq"));

            services.Configure<FileSettings>(configuration.GetSection("Logs"));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddHostedService<LoggerServiceReceiver>();

            services.AddScoped<IFileService, FileService>();
        }
    }
}
