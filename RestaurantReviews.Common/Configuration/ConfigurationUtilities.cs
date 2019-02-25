using Microsoft.Extensions.Configuration;
using System.IO;

namespace RestaurantReviews.Common.Configuration
{
    public static class ConfigurationUtilities
    {
        public static string RestaurantReviewsDbConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("RestaurantReviewsNpgSql");
            return connectionString;
        }
    }
}
