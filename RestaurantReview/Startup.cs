[assembly: Microsoft.Owin.OwinStartup(typeof(RestaurantReview.Web.Startup))]

namespace RestaurantReview.Web
{
    using System.Web.Mvc;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
