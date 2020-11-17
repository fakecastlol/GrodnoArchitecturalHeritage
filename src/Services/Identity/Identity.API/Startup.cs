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
        //private const string  

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //string connection = "Server=.\\SQLExpress;Trusted_Connection=Yes;Database=UserDB;";
            string connection = Configuration.GetConnectionString("DbConnection");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddTransient<DbContext, UserContext>();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddAutoMapper(Assembly.Load("Identity.Infrastructure.Business"), Assembly.Load("Identity.API"));

            services.AddScoped<IUserService, UserService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
