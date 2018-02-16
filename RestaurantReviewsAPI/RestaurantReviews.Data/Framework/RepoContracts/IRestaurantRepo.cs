using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data.Framework.RepoContracts
{
    public interface IRestaurantRepo
    {
        void Add(Restaurant restaurant);
        Restaurant Get(long restaurantId);
        Task<List<Restaurant>> FindMatchingResults(string name = null, string city = null, string stateCode = null, string stateName = null, int skip = 0, int take = 500);
    }
}
