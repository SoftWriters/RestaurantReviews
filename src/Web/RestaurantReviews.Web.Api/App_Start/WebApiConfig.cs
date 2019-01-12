using RestaurantReviews.Common;
using RestaurantReviews.Web.Api.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Registration;
using Unity.RegistrationByConvention;

namespace RestaurantReviews.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            //container.RegisterTypes(
            //    AllClasses.FromLoadedAssemblies(),
            //    WithMappings.FromAllInterfaces,
            //    WithName.Default,
            //    WithLifetime.Transient,
            //    type =>
            //    {
            //        return new InjectionMember[0];
            //    });
            AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("RestaurantReviews")).ToList().ForEach(asm =>
            {
                container.RegisterTypes(asm.GetTypes(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.PerResolve);
            });

            var configProvider = container.Resolve<IConfigurationProvider>();
            var dbContext = new DbContext() { ConnnectionString = configProvider.ConnnectionString };
            container.RegisterInstance(typeof(IDbContext), dbContext, new ContainerControlledLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        //public static void RegisterComponents()
        //{
        //    var container = new UnityContainer();
        //    var repositoryAssembly = AppDomain.CurrentDomain.GetAssemblies()
        //        .First(a => a.FullName == "CManager.Repository, Version=X.X.X.X, Culture=neutral, PublicKeyToken=null");

        //    container.RegisterTypes(repositoryAssembly.GetTypes(),
        //        WithMappings.FromMatchingInterface,
        //        WithName.Default,
        //        WithLifetime.ContainerControlled);

        //    container.RegisterType<ApplicationDbContext>(new PerResolveLifetimeManager());
        //    // ................ register other things is needed
        //    DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        //}
    }
}
