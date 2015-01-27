using Abp.Web.Mvc.Controllers;

namespace RestaurantReviews.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class RestaurantReviewsControllerBase : AbpController
    {
        protected RestaurantReviewsControllerBase()
        {
            LocalizationSourceName = RestaurantReviewsConsts.LocalizationSourceName;
        }
    }
}