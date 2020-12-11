using System.Reflection;
using AutoMapper;
using Heritage.Domain.Interfaces.Repositories;
using Heritage.Infrastructure.Business.Services;
using Heritage.Infrastructure.Data.EFContext;
using Heritage.Infrastructure.Data.Repositories;
using Heritage.Services.Interfaces.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Heritage.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddAppConnections(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<HeritageContext>(options => options.UseSqlServer(connection, x => x.UseNetTopologySuite()) 
            );
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

            services.AddScoped<IConstructionService, ConstructionService>();
        }
    }
}