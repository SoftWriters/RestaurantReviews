using RestaurantReviewService.Concrete;
using RestaurantReviewService.Entities;
using RestaurantReviewService.Operations;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviewService.Strategy
{
    public interface IValidateNewRestaurantAdd : IRepositoryStrategy<RestaurantAddValidationParams, bool> { }

    public struct RestaurantAddValidationParams
    {
        public ISelectAllRestaurantsOperation SelectAllRestaurantsOperation { get; set; }

        public Restaurant RestaurantAddCandidate { get; set; }
    }

    public sealed class ValidateNewRestaurantAdd : IValidateNewRestaurantAdd
    {
        bool IRepositoryStrategy<RestaurantAddValidationParams, bool>.Execute(RestaurantAddValidationParams p)
        {
            bool okToAdd = true;

            IList<Restaurant> currentRestaurants = p.SelectAllRestaurantsOperation.Execute(new RestaurantsRepository());

            if(currentRestaurants.SingleOrDefault(x => x.Name == p.RestaurantAddCandidate.Name &&
            x.City == p.RestaurantAddCandidate.City &&
            x.StateIdRef == p.RestaurantAddCandidate.StateIdRef) != null)
            {
                okToAdd = false;
            }

            return okToAdd;
        }
    }
}
