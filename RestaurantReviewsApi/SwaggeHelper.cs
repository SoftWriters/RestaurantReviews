using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RestaurantReviews.Api
{

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A Swagger helper class to manage configuration. We'll do it here instead of in
    ///             the Startup class.  It's cleaner and separates concerns.</summary>
    ///
    ///-------------------------------------------------------------------------------------------------

    public static class SwaggerHelper
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Configure swagger generate. </summary>
        ///
        /// <param name="swaggerGenOptions">    Options for controlling the swagger generate. </param>
        ///-------------------------------------------------------------------------------------------------

        public static void ConfigureSwaggerGen(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.SwaggerDoc("v1", new Info
            {
                Version = "v1",
                Title = "Restaurant Reviews API",
                Description = "Softwariters' RestaurantReviews Challenge using ASP.NET Core Web 2 API",
                TermsOfService = "None",
                Contact = new Contact
                {
                    Name = "Chris Rickard",
                    Email = string.Empty,
                    Url = "https://www.linkedin.com/in/the-christopher-rickard/"
                },
            });
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Configure swagger. </summary>
        ///
        /// <param name="swaggerOptions">   Options for controlling the swagger. </param>
        ///-------------------------------------------------------------------------------------------------

        public static void ConfigureSwagger(SwaggerOptions swaggerOptions)
        {
            swaggerOptions.RouteTemplate = "api-docs/{documentName}/swagger.json";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Configure swagger user interface. </summary>
        ///
        /// <param name="swaggerUiOptions"> Options for controlling the swagger user interface. </param>
        ///-------------------------------------------------------------------------------------------------

        public static void ConfigureSwaggerUi(SwaggerUIOptions swaggerUiOptions)
        {
            swaggerUiOptions.SwaggerEndpoint($"/api-docs/v1/swagger.json", $"V1 Docs");
            swaggerUiOptions.RoutePrefix = "api-docs";
        }
    }
}