using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Softwriters.RestaurantReviews.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var ipAddress = IPAddress.Loopback;

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls()
                        .ConfigureKestrel(serverOptions =>
                        {
                            serverOptions.Listen(ipAddress, 8500);
                        })
                        .UseStartup<Startup>();
                });
        }
    }
}
