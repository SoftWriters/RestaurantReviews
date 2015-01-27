using Abp.Application.Services;

namespace RestaurantReviews
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class RestaurantReviewsAppServiceBase : ApplicationService
    {
        protected RestaurantReviewsAppServiceBase()
        {
            LocalizationSourceName = RestaurantReviewsConsts.LocalizationSourceName;
        }
    }
}