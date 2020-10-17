using RestaurantReviewsService.DomainModels;
using RestaurantReviewsService.ViewModels;
using System.Collections.Generic;

namespace RestaurantReviewsService.ModelBuilders.ViewModelBuilders
{
    public struct RestaurantVMBuilderParams
    {
        public RestaurantDM RestaurantDomainModel { get; set; }

        public IList<UserReviewDM> UserReviewsDMItems { get; set; }
    }

    public sealed class RestaurantViewModelBuilder : IModelBuilder<RestaurantViewModel, RestaurantVMBuilderParams>
    {
        private IModelBuilder<UserReviewViewModel, UserReviewDM> _userReviewViewModelBuilder;

        public RestaurantViewModelBuilder()
        {
            _userReviewViewModelBuilder = new UserReviewViewModelBuilder();
        }

        RestaurantViewModel IModelBuilder<RestaurantViewModel, RestaurantVMBuilderParams>.Build(RestaurantVMBuilderParams p)
        {
           
            // TODO - change p to a struct containing possibly restaurant reviews
            return new RestaurantViewModel
            {
                City = p.RestaurantDomainModel.City,
                Id = p.RestaurantDomainModel.Id,
                Name = p.RestaurantDomainModel.Name,
                Overview = p.RestaurantDomainModel.Overview,
                State = p.RestaurantDomainModel.State,
                StateId = p.RestaurantDomainModel.StateId,
                StreetAddress = p.RestaurantDomainModel.StreetAddress,
                UserReviews = BuildUserReviews(p.UserReviewsDMItems)
                
            };
        }

        private IList<UserReviewViewModel> BuildUserReviews(IList<UserReviewDM> items)
        {
            IList<UserReviewViewModel> results = new List<UserReviewViewModel>();

            if (items != null && items.Count > 0)
            {
                foreach (UserReviewDM userReviewDomainModel in items)
                {
                    results.Add(_userReviewViewModelBuilder.Build(userReviewDomainModel));
                }
            }

            return results;
        }
    }
}