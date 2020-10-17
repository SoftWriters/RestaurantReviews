using RestaurantReviewService.Entities;
using RestaurantReviewsService.DomainModels;
using RestaurantReviewsService.ViewModels;

namespace RestaurantReviewsService.ModelBuilders.DomainModelBuilders
{
    public sealed class UserReviewDomainModelBuilder : IModelBuilder<UserReviewDM, UserReview>,
        IModelBuilder<UserReviewDM,UserReviewViewModel>
    {
        /// <summary>  Builds the UserReview Domain Model from a UserReview Data Entity Model</summary>
        /// <param name="p">The p.</param>
        /// <returns>UserReviewDM</returns>
        UserReviewDM IModelBuilder<UserReviewDM, UserReview>.Build(UserReview p)
        {
            return new UserReviewDM
            {
                Id = p.Id,
                Comments = p.Comments,
                RatingsDescription = p.RatingsDescription,
                RatingsLevel = p.RatingsLevel,
                RatingsRef = p.RatingsRef,
                RestaurantIdRef = p.RestaurantIdRef,
                Title = p.Title,
                UserIdRef = p.UserIdRef
            };
        }

        /// <summary>Builds the UserReview Domain Model from a UserReviewViewModel</summary>
        /// <param name="p">The p.</param>
        /// <returns>UserReviewDM</returns>
        UserReviewDM IModelBuilder<UserReviewDM, UserReviewViewModel>.Build(UserReviewViewModel p)
        {
            return new UserReviewDM
            {
                Id = p.Id,
                Comments = p.Comments,
                RatingsDescription = p.RatingsDescription,
                RatingsLevel = p.RatingsLevel,
                RatingsRef = p.RatingsRef,
                RestaurantIdRef = p.RestaurantIdRef,
                Title = p.Title,
                UserIdRef = p.UserIdRef
            };
        }
    }
}