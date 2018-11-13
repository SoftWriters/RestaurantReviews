using System.Web.Http;
using Unity;
using Unity.WebApi;
using Repositories;

namespace RestaurantReview2
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterInstance<IChainRepository>(new ChainRepository());
            container.RegisterInstance<ICityRepository>(new CityRepository());
            container.RegisterInstance<IReviewRepository>(new ReviewRepository());
            container.RegisterInstance<IUserRepository>(new UserRepository());
            container.RegisterInstance<IRestaurantRepository>(new RestaurantRepository());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}