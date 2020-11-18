using System;
using System.Reflection;
using AutoMapper;
using Identity.Domain.Interfaces.Repositories;
using Identity.Infrastructure.Business.Services;
using Identity.Infrastructure.Data.EFContext;
using Identity.Infrastructure.Data.Repositories;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DbConnection");

            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));

            services.Configure<JwtSettings>(Configuration.GetSection("JwtKey"));

            services.AddTransient<DbContext, UserContext>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddAutoMapper(Assembly.Load("Identity.Infrastructure.Business"), Assembly.Load("Identity.API"));

            services.AddScoped<IUserService, UserService>();

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DbInitializer.Seed(serviceProvider);
        }
    }
}
