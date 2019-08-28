using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantReviews.Business.Managers;
using RestaurantReviews.Business.Validators;
using RestaurantReviews.Interfaces.Business;
using RestaurantReviews.Interfaces.Factories;
using RestaurantReviews.Interfaces.Models;
using RestaurantReviews.Interfaces.Repository;
using RestaurantReviews.JsonData;
using RestaurantReviews.JsonData.Factories;
using RestaurantReviews.JsonData.Repositories;

namespace RestaurantReviews.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Setup dependency injection
            services.AddScoped<IDataFactory, DataFactory>();
            services.AddScoped<IModelValidator<IReview>, ReviewModelValidator>();
            services.AddScoped<IContext, Context>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>(); 
            services.AddScoped<IReviewManager, ReviewManager>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
