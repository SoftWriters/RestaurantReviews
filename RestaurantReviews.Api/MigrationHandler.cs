using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace RestaurantReviews.Api
{
    public class MigrationHandler
    {
        public static void RunMigrations()
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString("Server=.;Database=RestaurantDB;User Id=RestaurantService;Password=MyReallyBadPassword1;")
                    .ScanIn(typeof(RestaurantReviews.Api.MigrationHandler).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}
