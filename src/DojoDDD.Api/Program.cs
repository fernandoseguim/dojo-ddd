using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Exceptions;

namespace DojoDDD.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            await host.InitAsync();
            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseSerilog((context, logger) =>
                {
                    logger.Enrich.FromLogContext();
                    logger.Enrich.WithExceptionDetails();
                    logger.Enrich.WithMachineName();
                    logger.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning);
                    logger.MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning);
                    logger.MinimumLevel.Override("MassTransit", Serilog.Events.LogEventLevel.Debug);
                    logger.Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("/ready")));
                    logger.Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("/hc")));
                    logger.ReadFrom.Configuration(context.Configuration);
                })
                .UseStartup<Startup>();
    }
}
