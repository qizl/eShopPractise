using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EnjoyCodes.eShopOnContainers.Services.IdentityAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args)
            //    .MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
            //    .MigrateDbContext<ApplicationDbContext>((context, services) =>
            //    {
            //        var env = services.GetService<IHostingEnvironment>();
            //        var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();
            //        var settings = services.GetService<IOptions<AppSettings>>();

            //        new ApplicationDbContextSeed()
            //            .SeedAsync(context, env, logger, settings)
            //            .Wait();
            //    })
            //    .MigrateDbContext<ConfigurationDbContext>((context, services) =>
            //    {
            //        var configuration = services.GetService<IConfiguration>();

            //        new ConfigurationDbContextSeed()
            //            .SeedAsync(context, configuration)
            //            .Wait();
            //    }).Run();

            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseHealthChecks("/hc")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
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
