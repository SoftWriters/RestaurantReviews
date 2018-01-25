using System.Collections.Generic;
using Abp.Domain.Repositories;

namespace RestaurantReviews
{
    public interface IRestaurantRepository : IRepository<Restaurant, long>
    {
        List<Restaurant> GetAllByCity(int? cityId);
    }
}
