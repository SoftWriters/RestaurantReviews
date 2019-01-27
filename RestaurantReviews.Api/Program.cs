using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RestaurantReviews.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args != null && args.Any() && args[0] == "migrate")
            {
                MigrationHandler.RunMigrations();
            }
            else
            {
                CreateWebHostBuilder(args).Build().Run();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
