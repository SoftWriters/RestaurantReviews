using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    // There's no DB backing this up, so I'm skipping any Unit of Work calls such as '_context.Commit();'
    public class RestaurantRepository : IRestaurantRepository
    {
        IEnumerable<IRestaurantModel> _restaurants = new List<IRestaurantModel>();

        int _maxId = 0;
        
        public IEnumerable<IRestaurantModel> AddRestaurant(IRestaurantModel restaurant)
        {
            // Do not allow duplicates - spec says 'Post a restaurant that is not in the database'
            // I would probably do this duplicate check in the javascript prior to sending it down here.  Though
            // there's something to be said for doing the check in the SQL INSERT stored procedure to minimize 
            // the risk of race conditions.
            bool isDuplicate = false; 
            foreach (var rest in _restaurants)
            {
                if (restaurant.Name == rest.Name && 
                    restaurant.City == rest.City &&
                    restaurant.Chain == rest.Chain &&
                    restaurant.Address == rest.Address)
                {
                    isDuplicate = true;
                }
            }

            if (!isDuplicate)
            {
                restaurant.Id = ++_maxId;
                List<IRestaurantModel> rests = _restaurants.ToList();
                rests.Add(restaurant);
                _restaurants = rests;
                // send to Entity Framework or other ORM.
            }

            // Return the array instead of a boolean to save a call.  Issue is there's no way to tell if
            // it actually succeeded, unless the DB code causes an exception which will float up.  There
            // is no error checking in this sample, but there should be.
            return _restaurants;
        }
        
        public IRestaurantModel GetRestaurantById(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<IRestaurantModel> GetRestaurants()
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
            return _restaurants.Any();
        }
    }
}