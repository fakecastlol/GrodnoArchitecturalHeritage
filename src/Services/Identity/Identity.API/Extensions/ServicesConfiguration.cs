using System.Reflection;
using AutoMapper;
using FluentValidation;
using Identity.Domain.Interfaces.Repositories;
using Identity.Infrastructure.Business.Services;
using Identity.Infrastructure.Business.Support.File;
using Identity.Infrastructure.Business.Support.Token;
using Identity.Infrastructure.Data.EFContext;
using Identity.Infrastructure.Data.Repositories;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers.AppSettings;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;
using Identity.Services.Interfaces.Validation.FluentValidation.Login;
using Identity.Services.Interfaces.Validation.FluentValidation.Register;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Identity.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddAppConnections(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
        }

        public static void AddConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtKey"));

            services.Configure<FileSettings>(configuration.GetSection("Images"));
        }

        public static void AddContexts(this IServiceCollection services)
        {
            services.AddScoped<DbContext, UserContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("Identity.Infrastructure.Business"), Assembly.Load("Identity.API"));
        }

        public static void AddServices(this IServiceCollection services)
        {

            services.AddScoped<IFileService, FileService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITokenService, TokenService>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<RegisterRequestModel>, RegisterValidator>();

            services.AddTransient<IValidator<LoginRequestModel>, LoginValidator>();
        }
    }
}