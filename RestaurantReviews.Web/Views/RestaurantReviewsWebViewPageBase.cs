using Abp.Web.Mvc.Views;

namespace RestaurantReviews.Web.Views
{
    public abstract class RestaurantReviewsWebViewPageBase : RestaurantReviewsWebViewPageBase<dynamic>
    {

    }

    public abstract class RestaurantReviewsWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected RestaurantReviewsWebViewPageBase()
        {
            LocalizationSourceName = RestaurantReviewsConsts.LocalizationSourceName;
        }
    }
}