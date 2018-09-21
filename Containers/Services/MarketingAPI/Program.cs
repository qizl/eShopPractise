using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EnjoyCodes.eShopOnContainers.Services.MarketingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args)
            //.MigrateDbContext<MarketingContext>((context, services) =>
            //{
            //    var logger = services.GetService<ILogger<MarketingContextSeed>>();

            //    new MarketingContextSeed()
            //        .SeedAsync(context, logger)
            //        .Wait();

            //}).Run();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseHealthChecks("/hc")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseWebRoot("Pics")
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var builtConfig = config.Build();

                    var configurationBuilder = new ConfigurationBuilder();

                    if (Convert.ToBoolean(builtConfig["UseVault"]))
                    {
                        configurationBuilder.AddAzureKeyVault(
                            $"https://{builtConfig["Vault:Name"]}.vault.azure.net/",
                            builtConfig["Vault:ClientId"],
                            builtConfig["Vault:ClientSecret"]);
                    }

                    configurationBuilder.AddEnvironmentVariables();

                    config.AddConfiguration(configurationBuilder.Build());
                })
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .UseApplicationInsights()
                .Build();
    }
}
