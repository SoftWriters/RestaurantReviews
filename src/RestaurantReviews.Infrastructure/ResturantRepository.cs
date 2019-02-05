using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantReviews.Domain;

namespace RestaurantReviews.Infrastructure
{
    /// <summary>
    /// In the real world, this class should be wired up to the database using any ORM tools such as entity framework or NHibernate or the ado.net code
    /// for the purpose of a demo, a 
    /// </summary>
    public class ResturantRepository : IResturantRepository
    {
        private static ConcurrentDictionary<Guid, Resturant> _resturantRepository;
        private static ConcurrentDictionary<Guid, City> _cityRepository;

        static ResturantRepository()
        {
            _resturantRepository = new ConcurrentDictionary<Guid, Resturant>();
            _cityRepository = new ConcurrentDictionary<Guid, City>();

            //Adding the bootstrap data
            List<City> cities = new List<City>(new City[]{
                new City(Guid.NewGuid(), "Pittsburgh", "Pennsylvania", "United States of America"),
                new City(Guid.NewGuid(), "Philadelphia", "Pennsylvania", "United States of America"),
                new City(Guid.NewGuid(), "Harrisburgh", "Pennsylvania", "United States of America"),
                new City(Guid.NewGuid(), "Atlanta", "Georgia", "United States of America"),
                new City(Guid.NewGuid(), "Wexford", "Pennsylvania", "United States of America"),
            });
            cities.ForEach(c => _cityRepository.GetOrAdd(c.CityId, c));

        }


        public async Task<Resturant> AddAsync(Resturant resturant)
        {
            return await Task.Run<Resturant>(() =>
            {
                return _resturantRepository.GetOrAdd(resturant.CityId, resturant);
            });
        }

        public async Task<Resturant> FindAsync(Guid resturantId)
        {
            return await Task.Run<Resturant>(() =>
            {
                _resturantRepository.TryGetValue(resturantId, out Resturant ret);
                return ret;
            });
        }

        public async Task<Resturant> UpdateAsync(Resturant resturant)
        {
            return await Task.Run<Resturant>(() =>
            {
                return _resturantRepository.AddOrUpdate(resturant.CityId, resturant, (a, b) => b);
            });
        }

        public async Task<IEnumerable<Resturant>> GetResturantsByCityAsync(Guid cityId)
        {
            return await Task.Run<IEnumerable<Resturant>>(() =>
                {
                    var res = new List<Resturant>(_resturantRepository.Values);
                    return res.FindAll(a => a.CityId == cityId);
                });

        }

        public async Task<IEnumerable<City>> SearchCitiesAsync(string searchString)
        {
            return await Task.Run<IEnumerable<City>>(() =>
            {
                return (new List<City>(_cityRepository.Values)).FindAll(a =>
                a.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) || a.StateOrProvince.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) || a.Country.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));
            });
        }
    }
}
