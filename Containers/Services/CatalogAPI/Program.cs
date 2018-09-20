using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EnjoyCodes.eShopOnContainers.Services.CatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args)
            //    .MigrateDbContext<CatalogContext>((context, services) =>
            //    {
            //        var env = services.GetService<IHostingEnvironment>();
            //        var settings = services.GetService<IOptions<CatalogSettings>>();
            //        var logger = services.GetService<ILogger<CatalogContextSeed>>();

            //        new CatalogContextSeed()
            //        .SeedAsync(context, env, settings, logger)
            //        .Wait();

            //    })
            //    .MigrateDbContext<IntegrationEventLogContext>((_, __) => { })
            //    .Run();

            BuildWebHost(args).Run();
        }
        
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseHealthChecks("/hc")
                .UseContentRoot(Directory.GetCurrentDirectory())
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
                .Build();
    }
}
