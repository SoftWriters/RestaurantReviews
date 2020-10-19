using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Bll.Providers;
using RestaurantReviewsApi.Bll.Translators;
using RestaurantReviewsApi.Bll.Validators;
using RestaurantReviewsApi.Constants;
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

            services.AddDbContext<RestaurantReviewsContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            services.AddSingleton<IApiModelTranslator, ApiModelTranslator>();
            services.AddScoped<IRestaurantManager, RestaurantManager>();
            services.AddScoped<IReviewManager, ReviewManager>();
            ConfigureValidators(services);
            ConfigureAuth(services);
            ConfigureSwagger(services);
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant Reviews Api");
            });

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

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Restaurant Reviews Api",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

               c.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                 new OpenApiSecurityScheme
                 {
                   Reference = new OpenApiReference
                   {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                   }
                  },
                  new string[] { }
                }
              });

                //This line fixes a bug with how comments are parsed.
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            string issuer = Configuration["Jwt:Issuer"];
            string key = Configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(issuer))
                throw new Exception($"Missing config value for [Jwt:Issuer]");
            if (string.IsNullOrEmpty(key))
                throw new Exception($"Missing config value for [Jwt:Key]");

            services.AddScoped<IAuthProvider, AuthProvider>(x =>
            {
                return new AuthProvider(issuer, key);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.User, policy => policy.RequireClaim("scope", Scope.User));
                options.AddPolicy(Policy.Admin, policy => policy.RequireClaim("scope", Scope.Admin));
            });
        }
    }
}
