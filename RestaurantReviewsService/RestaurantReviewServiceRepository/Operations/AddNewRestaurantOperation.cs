using RestaurantReviewService.Abstract;
using RestaurantReviewService.Concrete;
using RestaurantReviewService.Entities;
using RestaurantReviewService.Strategy;
using System;

namespace RestaurantReviewService.Operations
{
    public interface IAddNewRestaurantsOperation : IRepositoryOperation<int, NewRestAddRepositoryOperationParams> { }

    public struct NewRestAddRepositoryOperationParams
    {
        public RestaurantsRepository RestaurantsRepository { get; set; }

        public Restaurant RestaurantAddCandidate { get; set; }
    }

    public sealed class AddNewRestaurantOperation : IAddNewRestaurantsOperation
    {
        public int AddNewRestaurant(Restaurant restaurant)
        {
            try
            {
                bool okToAddNewRestaurant = ValidateRestaurantAdd(restaurant);

                if (okToAddNewRestaurant == false)
                {
                    throw new Exception(string.Format("The Restaurant: {0} for City: {1} already exists in the database",
                        restaurant.Name, restaurant.City));
                }
                else
                {
                    return ((IAddNewRestaurantsOperation)this).Execute(new NewRestAddRepositoryOperationParams
                    {
                        RestaurantAddCandidate = restaurant,
                        RestaurantsRepository = new RestaurantsRepository()
                    });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        int IRepositoryOperation<int, NewRestAddRepositoryOperationParams>.Execute(NewRestAddRepositoryOperationParams p)
        {
            object result = ((ISqlLiteDbRepository)p.RestaurantsRepository).Insert(p.RestaurantAddCandidate);

            if (int.TryParse(result.ToString(), out int operationResult))
            {
                return operationResult;
            }
            else
            {
                throw new Exception("AddNewRestaurantOperation::Execute received an unexpected result");
            }
        }       

        private bool ValidateRestaurantAdd(Restaurant restaurantAddCandidate)
        {
            IValidateNewRestaurantAdd newRestauranAddValidator = new ValidateNewRestaurantAdd();

            return newRestauranAddValidator.Execute(new RestaurantAddValidationParams
            {
                RestaurantAddCandidate = restaurantAddCandidate,
                SelectAllRestaurantsOperation = new SelectAllRestaurantsOperation()
            });
        }
    }
}
