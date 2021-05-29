using System.Reflection;
using AutoMapper;
using Heritage.Domain.Interfaces.Repositories;
using Heritage.Infrastructure.Business.Services;
using Heritage.Infrastructure.Business.Support.File;
using Heritage.Infrastructure.Business.Support.RabbitMQ;
using Heritage.Infrastructure.Data.EFContext;
using Heritage.Infrastructure.Data.Repositories;
using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Helpers.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heritage.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddAppConnections(this IServiceCollection services, IConfiguration configuration)
        {
            //var connection = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<HeritageContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DbConnection"), x => x.UseNetTopologySuite()) 
            );
        }

        public static void AddConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMQ"));

            services.Configure<FileSettings>(configuration.GetSection("Images"));
        }

        public static void AddContexts(this IServiceCollection services)
        {
            services.AddScoped<DbContext, HeritageContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IConstructionRepository, ConstructionRepository>();

            services.AddTransient<IImageRepository, ImageRepository>();
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("Heritage.Infrastructure.Business"), Assembly.Load("Heritage.API"));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQService, RabbitMQService>();

            services.AddScoped<IFileService, FileService>();

            services.AddScoped<IConstructionService, ConstructionService>();

            services.AddScoped<IImageService, ImageService>();
        }
    }
}