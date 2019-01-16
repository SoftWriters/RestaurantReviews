using RestaurantReviews.Common;
using RestaurantReviews.Web.Api.App_Start;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace RestaurantReviews.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();

            // auto map interfaces to classes in Unity container by convention
            RegisterUnityAutoResolutionMappings(container);

            //register dbcontext instance in Unity container for use by DAL
            RegisterDbContext(container);

            //register Unity container as WebAPI dependency resolver for injection into controllers
            config.DependencyResolver = new UnityHierarchicalDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }

        private static void RegisterDbContext(IUnityContainer container)
        {
            var configProvider = container.Resolve<IConfigurationProvider>();
            var dbContext = new DbContext() { ConnnectionString = configProvider.ConnnectionString };
            container.RegisterInstance(typeof(IDbContext), dbContext, new ContainerControlledLifetimeManager());
        }

        private static void RegisterUnityAutoResolutionMappings(IUnityContainer container)
        {
            ForceLoadAssemblies();

            AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("RestaurantReviews")).ToList().ForEach(asm =>
            {
                container.RegisterTypes(asm.GetTypes(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.PerResolve);
            });
        }
        private static void ForceLoadAssemblies()
        {
            foreach (var fileName in Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "RestaurantReviews*.dll"))
            {
                string assemblyName = Path.GetFileNameWithoutExtension(fileName);
                if (assemblyName != null)
                {
                    Assembly.Load(assemblyName);
                }
            }
        }
    }
}
