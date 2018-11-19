using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantReviews.Data;
using RestaurantReviews.Data.Interfaces;
using RestaurantReviews.Data.Repos;
using RestaurantReviews.Services;

namespace RestaurantReviews.Api
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>The Startup class configures the application and services. </summary>
    ///-------------------------------------------------------------------------------------------------

    public class Startup
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor performs setup and configuration for the Web API. </summary>
        ///
        /// <param name="configuration">    The configuration. </param>
        ///-------------------------------------------------------------------------------------------------

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the configuration. </summary>
        ///
        /// <value> The configuration. </value>
        ///-------------------------------------------------------------------------------------------------
        public IConfiguration Configuration { get; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>This method gets called by the runtime. Use this method to add services to the 
        ///          container.</summary>
        ///
        /// <param name="services"> The services. </param>
        ///-------------------------------------------------------------------------------------------------

        public void ConfigureServices(IServiceCollection services)
        {
            // this is where all of the magic occurs.  By adding our database context here along with adding
            // our repositories and services, the Web API framework injects the correct dependency into the
            // controllers and repos at run time.
            services.AddDbContext<RestaurantReviewContext>(options => options.UseInMemoryDatabase("RestatuarantReviews").EnableSensitiveDataLogging());
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IReviewService, ReviewService>();

            //our services need Model_View_Controller functionality
            services.AddMvc();

            //to generate API documentation automagically - tried using ReDoc instead of the Swagger UI for this exercise.
            services.AddSwaggerGen(SwaggerHelper.ConfigureSwaggerGen);
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>This method gets called by the runtime. Use this method to configure the HTTP request 
        ///          pipeline.</summary>
        ///
        /// <param name="app">  The application. </param>
        /// <param name="env">  The environment. </param>
        ///-------------------------------------------------------------------------------------------------

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // captures Exceptions instances and generates HTML Error repsonses
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // adds middleware for HSTS, which adds the header for Strict-Transport-Security headers
                app.UseHsts();
            }

            app.UseStaticFiles();

            // After this call, we'll be able to view the generated Swagger JSON at "/swagger/v1/swagger.json."
            app.UseSwagger();

            // the follwoing inserts the swagger-ui middleware if you want to expose interactive documentation, 
            // specifying the Swagger JSON endpoint(s) to power it from.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant Reviews API v1");
                //c.RoutePrefix = string.Empty;
            });

            // adds Model-View-Controller functionality to the execution pipeline
            app.UseMvc();
        }
    }
}

