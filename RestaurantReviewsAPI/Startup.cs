using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Repositories.Implementations;
using Repositories.Interfaces;

namespace RestaurantReviewsAPI
{
  public class Startup
  {
    private readonly ILogger _logger;
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      //Create local logging instance for statup class
      var nlogLoggerProvider = new NLogLoggerProvider();
      _logger = nlogLoggerProvider.CreateLogger(typeof(Startup).FullName);
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var dbType = Configuration.GetValue("DbProvider", "");
      var connectionString = Configuration.GetConnectionString("DefaultConnection");

      switch (dbType)
      {
        case "SQLServer":
          services.AddSingleton<IRestaurantRepo>(x => new RestaurantRepoEF(connectionString));
          services.AddSingleton<IReviewRepo>(x => new ReviewRepoEF(connectionString));
          break;
        case "MongoDb":
          /**The below repos are not actually implemented, but I wanted to give you an
           * idea of why I decided to abstract away EF Core through Repository Interfaces. This
           * way we could swap out and use a nosql db provider not supported by EF Core.  I'd 
           * imagine if we were actually developing a new yelp-type app we would want to use a nosql db
           * for scalablity. Regardless, the option to use a different ORM and DB PRovider is pretty useful.
           * **/
          services.AddSingleton<IRestaurantRepo>(x => new RestaurantRepoMongoDb(connectionString));
          services.AddSingleton<IReviewRepo>(x => new ReviewRepoMongoDb(connectionString));
          break;
        case "":
          _logger.LogError(String.Format("DbProvider not set in appsettings.json", dbType));
          throw new ConfigurationException(String.Format("DbProvider not set in appsettings.json", dbType));
        default:
          _logger.LogError(String.Format("Database Provider {0} not yet supported", dbType));
          throw new ConfigurationException(String.Format("Database Provider {0} not yet supported", dbType));
      };

      services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
