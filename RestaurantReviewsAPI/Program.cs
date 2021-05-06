using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Events;

namespace RestaurantReviewsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* start logging at earliest point in app */
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(new RenderedCompactJsonFormatter(), "log.json")
                .CreateLogger();

            try
            {
                Log.Information("Application starting up");

                var host = CreateHostBuilder(args)
                    .UseSerilog()
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
