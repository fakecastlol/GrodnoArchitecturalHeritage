using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;

namespace Web.Platform.HttpAggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        //.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                        .AddOcelot(Path.Combine("Routes"), hostingContext.HostingEnvironment as IWebHostEnvironment)
                        //.AddJsonFile("Routes/ocelotIdentityService.json")
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
