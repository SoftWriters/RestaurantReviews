using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
    // There's no DB backing this up, so I'm skipping any Unit of Work calls such as '_context.Commit();'
    public class CityRepository : ICityRepository
    {
        IEnumerable<ICityModel> _cities = new List<ICityModel>();

        public bool HasData()
        {
            return _cities.Count() > 0;
        }

        public IEnumerable<ICityModel> AddCity(ICityModel city)
        {
            List<ICityModel> cities = _cities.ToList();
            cities.Add(city);
            _cities = cities;
            return _cities;
        }

        public ICityModel FindCityById(int id)
        {
            return _cities.FirstOrDefault(r => r.Id == id);
        }
    }
}