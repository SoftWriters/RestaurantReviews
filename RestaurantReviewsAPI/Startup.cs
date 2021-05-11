using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repositories.Implementations;
using Repositories.Interfaces;

namespace RestaurantReviewsAPI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
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
           * imagine if we were actually developing this app we would probably want to use a nosql db
           * for scalablity
           * **/
          services.AddSingleton<IRestaurantRepo>(x => new RestaurantRepoMongoDb(connectionString));
          services.AddSingleton<IReviewRepo>(x => new ReviewRepoMongoDb(connectionString));
          break;
        case "":
          throw new Exception(String.Format("DbProvider not set in appsettings.json", dbType));
        default:
          throw new Exception(String.Format("Database Provider {0} not yet supported", dbType));
      };

      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
