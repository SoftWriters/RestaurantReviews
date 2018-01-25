using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RestaurantReviews.WebApi.Startup))]

namespace RestaurantReviews.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
