using System.Reflection;
using Abp.Modules;

namespace RestaurantReviews
{
    public class RestaurantReviewsCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
