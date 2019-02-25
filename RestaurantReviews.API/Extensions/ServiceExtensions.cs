using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantReviews.API.Helpers;
using RestaurantReviews.Common.Logging;
using RestaurantReviews.Data;
using RestaurantReviews.Data.Contracts.Logging;
using RestaurantReviews.Data.Contracts.Repositories;
using RestaurantReviews.Data.Repositories;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            //ToDo: Investigate why this reference is undefined.
            //services.AddAutoMapper();

            // Workaround - for now
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        /// <summary>
        /// Configure CORS policy  (Cross-Origin Resource Sharing)
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

                // More restrictive cors policy as example
                //options.AddPolicy("CorsPolicy",
                //    builder => builder.WithOrigins("http://www.something.com")
                //    .AllowAnyMethod()
                //    .WithHeaders("accept", "content-type")
                //    .AllowCredentials());

            });
        }

        /// <summary>
        /// Configure IIS Integration
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        /// <summary>
        /// Configure the logging service
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            // AddScoped - once per http request
            // AddTransient - once per call to logger service
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// Add the Repository context to the IOC container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureRepositoryContext(this IServiceCollection services, IConfiguration config)
        {
            // When using SQL server
            //services.AddDbContext<RepositoryContext>
            //    (options => options.UseSqlServer(Utilities.RestaurantReviewsDbConnectionString()));

            // When using Postres
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<RestaurantReviewsContext>()
                .BuildServiceProvider();
        }

        /// <summary>
        /// Add the Repository Wrapper to the IOC container
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        /// <summary>
        /// Register the Swagger generator, defining the swagger document(s)
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwaggerGenerator(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info {
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Email = "costarellamike@gmail.com",
                        Name = "Mike Costarella",
                        Url = "https://www.linkedin.com/in/mikecostarella/"
                    },
                    Description = "Web API Services to manage restaurant reviews.",
                    Title = "RestaurantReviews API",
                    Version = "v1"
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
