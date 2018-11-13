using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Newtonsoft.Json;

namespace Repositories
{
    // There's no DB backing this up, so I'm skipping any Unit of Work calls such as '_context.Commit();'
    public class RestaurantRepository : IRestaurantRepository
    {
        ICityRepository _cityRepository;
        IChainRepository _chainRepository;

        IEnumerable<IRestaurantModel> _restaurants = new List<IRestaurantModel>();

        public RestaurantRepository(ICityRepository cityRepo, IChainRepository chainRepo)
        {
            _cityRepository = cityRepo;
            _chainRepository = chainRepo;
        }

        public IEnumerable<IRestaurantModel> AddRestaurant(IRestaurantModel restaurant)
        {
            List<IRestaurantModel> rests = _restaurants.ToList();
            rests.Add(restaurant);
            _restaurants = rests;
            return _restaurants;
        }

        public IRestaurantModel GetRestaurant(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<IRestaurantModel> GetAllRestaurants()
        {
            return _restaurants;
        }

        public IEnumerable<IRestaurantModel> GetRestaurantsByCity(ICityModel city)
        {
            List<IRestaurantModel> rests = new List<IRestaurantModel>();
            rests.AddRange(_restaurants.Where(r => r.City == city));
            return rests;
        }

        public bool HasData()
        {
            return _restaurants.Count() > 0;
        }
    }
}