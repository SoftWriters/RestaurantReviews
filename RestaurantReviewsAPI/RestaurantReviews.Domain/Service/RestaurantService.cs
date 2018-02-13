using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Framework.UnitOfWorkContracts;
using RestaurantReviews.Data.Models;
using RestaurantReviews.Domain.Codes;
using RestaurantReviews.Domain.Models;

namespace RestaurantReviews.Domain.Service
{
    public class RestaurantService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public RestaurantService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
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
    }
}
