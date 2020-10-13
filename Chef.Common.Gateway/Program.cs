using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Chef.Common.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);

            // Ocelot configuration file
            builder.ConfigureAppConfiguration((hostingContext, config) => config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json"))
            .ConfigureServices(s =>
            {
                s.AddSingleton(builder);
                s.AddOcelot();
            })
            .UseSerilog((_, config) =>
            {
                config
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day);
            })
            .Configure(app =>
            {
                app.UseMiddleware<RequestResponseLoggingMiddleware>();
                app.UseOcelot().Wait();
            });

            var host = builder.Build();

            return host;
        }
    }
}
