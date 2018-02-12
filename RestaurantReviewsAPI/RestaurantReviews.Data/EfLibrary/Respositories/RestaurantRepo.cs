using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.EfLibrary.Context;
using RestaurantReviews.Data.EfLibrary.Entities;
using RestaurantReviews.Data.Framework.RepoContracts;
using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data.EfLibrary.Respositories
{
    public class RestaurantRepo : IRestaurantRepo
    {
        private readonly RestaurantReviewsContext _context;

        public RestaurantRepo(RestaurantReviewsContext context)
        {
            _context = context;
        }

        public void Add(Restaurant restaurant)
        {
            var state = _context
                .States
                .Find(restaurant.State_Id);

            _context
                .Restaurants
                .Add(new RestaurantDBO
                {
                    Name = restaurant.Name,
                    City = restaurant.City,
                    State = state
                });
        }
    }
}
