using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Models;
using RestaurantReviews.Data.Models.Domain;

namespace RestaurantReviews.Data.Framework.RepoContracts
{
    public interface IRestaurantRepo
    {
        void Add(Restaurant restaurant);
        Task<bool> Exists(string name = null, string city = null, string stateCode = null);
    }
}
