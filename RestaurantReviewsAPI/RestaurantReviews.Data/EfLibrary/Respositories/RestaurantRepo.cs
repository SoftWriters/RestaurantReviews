using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.EfLibrary.Context;
using RestaurantReviews.Data.EfLibrary.Entities;
using RestaurantReviews.Data.Framework.RepoContracts;
using RestaurantReviews.Data.Models;
using RestaurantReviews.Data.Models.Domain;

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
                .FirstOrDefault(x => x.Code == restaurant.StateCode);

            _context
                .Restaurants
                .Add(new RestaurantDBO
                {
                    Name = restaurant.Name,
                    City = restaurant.City,
                    State = state
                });
        }

        public Task<bool> Exists(string name = null, string city = null, string stateCode = null)
        {
            var query = _context
                .Restaurants
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query.Where(restaraunt => restaraunt.Name == name);

            if (!string.IsNullOrWhiteSpace(city))
                query.Where(restaraunt => restaraunt.City == city);

            if (!string.IsNullOrEmpty(stateCode))
                query.Where(restaurant => restaurant.State.Code == stateCode);

            return query
                .AnyAsync();
        }
    }
}
