using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Softwriters.RestaurantReviews.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Softwriters Restaurant Reviews API",
                    Description = "Use the Softwriters Restaurant Reviews REST API to create, read, update and delete reviews.",
                    TermsOfService = new Uri("http://www.example.com/terms-of-service/"),
                    Contact = new OpenApiContact
                    {
                        Name = "William Napier",
                        Email = "wnapier@example.com",
                        Url = new Uri("https://github.com/williamdnapier"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("http://www.example.com/license/"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Softwriters Restaurant Reviews API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}