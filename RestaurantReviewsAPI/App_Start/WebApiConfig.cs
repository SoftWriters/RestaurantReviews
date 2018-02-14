using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RestaurantReviewsAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetRestaurantsByCity",
                routeTemplate: "api/{controller}/{action}/{city}/{stateId}"
						);

            config.Routes.MapHttpRoute(
                name: "GetReviewsByUser",
                routeTemplate: "api/{controller}/{action}/{username}"
            );
        }
    }
}
