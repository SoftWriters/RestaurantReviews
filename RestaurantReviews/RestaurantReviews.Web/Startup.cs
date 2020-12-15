using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantReviews.Config;
using RestaurantReviews.Data;
using RestaurantReviews.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews
{
    public class Startup
    {
        public AppConfiguration Configuration { get; } = new AppConfiguration();

        public Startup(IConfiguration configuration)
        {
            configuration.Bind(Configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddDbContext<RestaurantContext>(p =>
            {
                p.UseSqlServer(Configuration.ConnectionStrings.Default);
            });

            services.AddControllers();
            services.AddSwaggerGen();

            // Application-specific services
            services.UseRestaurantReviewsEFCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RestaurantContext context)
        {
            app.UseSwagger(c =>
            {
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            // If enabled, the database will be dropped/recreated each time
            // Turn this off by changing the DropDb environment variable in the project properties
            if (Configuration.DropDb && Debugger.IsAttached)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
