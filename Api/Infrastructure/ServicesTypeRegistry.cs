using Microsoft.Extensions.DependencyInjection;
using Softwriters.RestaurantReviews.Services;
using Softwriters.RestaurantReviews.Services.Helpers;
using Softwriters.RestaurantReviews.Services.Interfaces;

namespace Softwriters.RestaurantReviews.Api.Infrastructure
{
    public static class ServicesTypeRegistry
    {
        public static void UpdateServiceCollection(IServiceCollection services)
        {
            services.AddScoped<IServiceHelper, ServiceHelper>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICriticService, CriticService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IRestaurantTypeService, RestaurantTypeService>();
            services.AddScoped<IReviewService, ReviewService>();
        }
    }
}
