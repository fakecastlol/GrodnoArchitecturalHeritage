using Heritage.API.Extensions;
using Heritage.Infrastructure.Data.EFContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Heritage.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppConnections(_configuration);

            services.AddConfigures(_configuration);

            services.AddContexts();

            services.AddRepositories();

            services.AddMappers();

            services.AddServices();

            services.AddDirectoryBrowser();

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            var path = Path.Combine(env.ContentRootPath, "objects");

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = path,
                EnableDirectoryBrowsing = true
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DbInitializer.Seed(serviceProvider);
        }
    }
}
