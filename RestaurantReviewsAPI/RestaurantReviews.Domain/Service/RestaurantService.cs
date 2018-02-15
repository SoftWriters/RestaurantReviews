using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Framework.UnitOfWorkContracts;
using RestaurantReviews.Data.Models;
using RestaurantReviews.Domain.Codes;
using RestaurantReviews.Domain.Const;
using RestaurantReviews.Domain.Models;

namespace RestaurantReviews.Domain.Service
{
    public class RestaurantService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly long _idOfActiveUser;

        public RestaurantService(IUnitOfWorkFactory unitOfWorkFactory, long idOfActiveUser)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _idOfActiveUser = idOfActiveUser;
        }


        public async Task<OperationResponse> AddRestaurant(Restaurant restaurant)
        {
            var unitOfWork = _unitOfWorkFactory.Get();
            
            var stateExists = await unitOfWork
                .StateRepo
                .Exists(restaurant.StateCode);

            if(!stateExists)
                return new OperationResponse
                {
                    OpCode = OpCodes.InvalidOperation,
                    Message = $"Unrecognized state code {restaurant.StateCode}."
                };

            var identicalRestaurants = await unitOfWork
                .RestaurantRepo
                .FindMatchingResults(restaurant.Name, restaurant.City, restaurant.StateCode);

            if (identicalRestaurants.Any())
                return new OperationResponse
                {
                    OpCode = OpCodes.InvalidOperation,
                    Message = "Identical record exists."
                };

            unitOfWork
                .RestaurantRepo
                .Add(restaurant);

            await unitOfWork
                .CommitAsync();

            return new OperationResponse
            {
                OpCode = OpCodes.Success
            };
        }


        public async Task<List<Restaurant>> QueryRestaurants(string name = null, string stateCode = null, string stateName = null, string city = null, int skip = 0, int take = QueryConstants.DefaultPageSize)
        {
            var unitOfWork = _unitOfWorkFactory
                .Get();

            if (take > QueryConstants.MaxPageSize)
                take = QueryConstants.MaxPageSize;

            var filteredResults = await unitOfWork
                .RestaurantRepo
                .FindMatchingResults(
                    skip: skip,
                    take: take,
                    name: name,
                    stateCode: stateCode,
                    stateName: stateName,
                    city: city
                );

            return filteredResults;
        }

        public async Task<OperationResponse> AddReviewToRestaurant(long restaurantId, Review review)
        {
            var unitOfWork = _unitOfWorkFactory.Get();

            if (review.Stars > 5)
                review.Stars = 5;
            if (review.Stars < 0)
                review.Stars = 0;

            var restaurant = unitOfWork
                .RestaurantRepo
                .Get(restaurantId);

            if(restaurant == null)
                return new OperationResponse
                {
                    OpCode = OpCodes.InvalidOperation,
                    Message = "Restaurant ID invalid."
                };

            var user = unitOfWork
                .UserRepo
                .Get(_idOfActiveUser);

            if(user == null)
                throw new Exception("No account found matching active user's id.");

            var existingReviews = await unitOfWork
                .ReviewRepo
                .FindMatchingResults(restaurantId, _idOfActiveUser);

            if(existingReviews.Any())
                return new OperationResponse
                {
                    OpCode = OpCodes.InvalidOperation,
                    Message = "User has already submitted a review for this restaurant."
                };

            unitOfWork
                .ReviewRepo
                .Add(restaurantId, _idOfActiveUser, review);

            await unitOfWork
                .CommitAsync();

            return new OperationResponse
            {
                OpCode = OpCodes.Success
            };
        }
    }
}
