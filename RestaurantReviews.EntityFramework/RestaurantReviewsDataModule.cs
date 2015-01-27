using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using RestaurantReviews.EntityFramework;

namespace RestaurantReviews
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(RestaurantReviewsCoreModule))]
    public class RestaurantReviewsDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<RestaurantReviewsDbContext>(null);
        }
    }
}
