using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace RestaurantRating
{
    public class CustomJsonFormatter : JsonMediaTypeFormatter
    {
        public CustomJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
            name: "Restaurant",
            routeTemplate: "api/restaurant/{id}",
            defaults: new { controller = "restaurant", id = RouteParameter.Optional }

        );

            config.Routes.MapHttpRoute(
           name: "RestaurantByCity",
           routeTemplate: "api/restaurantbycity/{id}",
           defaults: new { controller = "restaurantbycity", id = RouteParameter.Optional }

       );

            config.Routes.MapHttpRoute(
            name: "ReviewsByUser",
            routeTemplate: "api/reviewsbyuser/{id}",
            defaults: new { controller = "reviewsbyuser", id = RouteParameter.Optional }

        );
            config.Routes.MapHttpRoute(
            name: "Users",
            routeTemplate: "api/users/{id}",
            defaults: new { controller = "users", id = RouteParameter.Optional }

        );
            config.Routes.MapHttpRoute(
           name: "NameRestaurant",
           routeTemplate: "api/namerestaurant/{id}",
           defaults: new { controller = "namerestaurant", id = RouteParameter.Optional }

       );

            config.Routes.MapHttpRoute(
             name: "DefaultApi",
             routeTemplate: "api/{controller}/{id}",
             defaults: new { id = RouteParameter.Optional }
         );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
            config.Formatters.Add(new CustomJsonFormatter());
        }
    }
}
