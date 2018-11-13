using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    // There's no DB backing this up, so I'm skipping any Unit of Work calls such as '_context.Commit();'
    public class CityRepository : ICityRepository
    {
        IEnumerable<ICityModel> _cities = new List<ICityModel>();

        int _maxId;

        public bool HasData()
        {
            return _cities.Any();
        }

        public IEnumerable<ICityModel> AddCity(ICityModel city)
        {
            List<ICityModel> cities = _cities.ToList();
            city.Id = ++_maxId;
            cities.Add(city);
            _cities = cities;
            return _cities;
        }

        public ICityModel GetCityById(int id)
        {
            return _cities.FirstOrDefault(r => r.Id == id);
        }
    }
}