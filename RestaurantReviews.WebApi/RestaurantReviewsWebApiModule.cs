using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace RestaurantReviews
{
    [DependsOn(typeof(AbpWebApiModule), typeof(RestaurantReviewsApplicationModule))]
    public class RestaurantReviewsWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(RestaurantReviewsApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
