using System.Reflection;
using Abp.Modules;

namespace RestaurantReviews
{
    [DependsOn(typeof(RestaurantReviewsCoreModule))]
    public class RestaurantReviewsApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
