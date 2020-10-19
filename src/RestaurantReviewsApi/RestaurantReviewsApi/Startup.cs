using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSwag;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Bll.Translators;
using RestaurantReviewsApi.Bll.Validators;
using RestaurantReviewsApi.Entities;

namespace RestaurantReviewsApi
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
            services.AddControllers();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddOpenApiDocument(config =>
            {
                config.DocumentName = "v1";
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Restaurant Reviews Api";
                    document.Schemes = new List<OpenApiSchema>() { OpenApiSchema.Http, OpenApiSchema.Https };
                };
            });

            services.AddDbContext<RestaurantReviewsContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            services.AddSingleton<IApiModelTranslator, ApiModelTranslator>();
            services.AddScoped<IRestaurantManager, RestaurantManager>();
            services.AddScoped<IReviewManager, ReviewManager>();
            ConfigureValidators(services);
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
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddSingleton<IValidator<RestaurantApiModel>, RestaurantApiModelValidator>();
            services.AddSingleton<IValidator<ReviewApiModel>, ReviewApiModelValidator>();
            services.AddSingleton<IValidator<RestaurantSearchApiModel>, RestaurantSearchApiModelValidator>();
            services.AddSingleton<IValidator<ReviewSearchApiModel>, ReviewSearchApiModelValidator>();

        }
    }
}
