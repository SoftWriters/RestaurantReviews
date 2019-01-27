using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
//using NJsonSchema;
//using NSwag.AspNetCore;
using RestaurantReviews.Api.DataAccess;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRestaurantValidator, RestaurantValidator>();
            services.AddTransient<IRestaurantQuery, RestaurantQuery>();
            services.AddTransient<IInsertRestaurant, InsertRestaurant>();
            services.AddTransient<IReviewValidator, ReviewValidator>();
            services.AddTransient<IReviewQuery, ReviewQuery>();
            services.AddTransient<IInsertReview, InsertReview>();
            services.AddTransient<IDeleteReview, DeleteReview>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Restaurant Reviews";
                    document.Info.Description = "A simple API for managing restaurant reviews";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Eric Kepes",
                        Email = "eric@kepes.net",
                        Url = "https://github.com/ekepes"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}
