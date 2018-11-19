using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace RestaurantReviews.Api
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   The Program class contains the main() entry point. </summary>
    ///-------------------------------------------------------------------------------------------------

    public class Program
    {
        private static IConfiguration _loggingConfiguration;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Main entry-point for this application. </summary>
        ///
        /// <param name="args"> An array of command-line argument strings. </param>
        ///
        /// <returns>   Exit-code for the process - 0 for success, else an error code. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static int Main(string[] args)
        {
            // SetupLogger sets up logger to console.  In a robust app, I'd log to something like ElasticStack
            // we want to get logging working before doing anything else.  We could setup logging via a 
            // deletegate in the ".UserSerilog()" call below.
            SetupLogger();

            //
            try
            {
                // log informational message about starting our web host
                Log.Information("Starting Web Host");
                CreateWebHostBuilder(args)
                    .UseSerilog() 
                    .Build()
                    .Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);
                return 1;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets up the logger. </summary>
        ///-------------------------------------------------------------------------------------------------

        private static void SetupLogger()
        {
            _loggingConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Config/logsettings.json", true, false)
                .AddEnvironmentVariables()
                .Build();

            // logger is configured in the json config file referenced above.
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_loggingConfiguration)
                .CreateLogger();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates web host builder. </summary>
        ///
        /// <param name="args"> An array of command-line argument strings. </param>
        ///
        /// <returns>   The new web host builder. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
