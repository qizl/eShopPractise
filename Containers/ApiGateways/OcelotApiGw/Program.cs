using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EnjoyCodes.eShopOnContainers.ApiGateways.OcelotApiGw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddJsonFile("configuration.json", false, false)
                    .AddEnvironmentVariables();
                })
                //.ConfigureServices(s =>
                //{
                //    s.AddOcelot();
                //})
                //.ConfigureLogging((hostingContext, logging) =>
                //{
                //    //add your logging
                //})
                //.UseIISIntegration()
                //.Configure(app =>
                //{
                //    app.UseOcelot().Wait();
                //})
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
