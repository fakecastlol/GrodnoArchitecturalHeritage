using AutoMapper;
using FluentValidation;
using Identity.Domain.Interfaces.Repositories;
using Identity.Infrastructure.Business.Services;
using Identity.Infrastructure.Business.Support.Email;
using Identity.Infrastructure.Business.Support.File;
using Identity.Infrastructure.Business.Support.Header;
using Identity.Infrastructure.Business.Support.Password;
using Identity.Infrastructure.Business.Support.RabbitMQ;
using Identity.Infrastructure.Business.Support.Token;
using Identity.Infrastructure.Data.EFContext;
using Identity.Infrastructure.Data.Repositories;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers.Options;
using Identity.Services.Interfaces.Helpers.Rabbit;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;
using Identity.Services.Interfaces.Validation.FluentValidation.Login;
using Identity.Services.Interfaces.Validation.FluentValidation.Register;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static void AddAppConnections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));
        }

        public static void AddConfigures(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtKey"));

            services.Configure<FileSettings>(configuration.GetSection("Images"));

            services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMQ"));

            services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
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
            services.AddSingleton<IRabbitMQService, RabbitMQService>();

            services.AddScoped<IHeaderService, HeaderService>();

            services.AddScoped<IPasswordService, PasswordService>();

            services.AddScoped<IFileService, FileService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IEmailService, EmailService>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<RegisterRequestModel>, RegisterValidator>();

            services.AddTransient<IValidator<LoginRequestModel>, LoginValidator>();
        }
    }
}