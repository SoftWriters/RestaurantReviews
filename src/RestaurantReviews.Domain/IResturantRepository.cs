using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Domain
{
    public interface IResturantRepository
    {
        Task<Resturant> AddAsync(Resturant resturant);

        Task<Resturant> UpdateAsync(Resturant resturant);

        Task<Resturant> FindAsync(Guid resturantId);

        Task<IEnumerable<Resturant>> GetResturantsByCityAsync(Guid cityId);

        Task<IEnumerable<City>> SearchCitiesAsync(string searchString);
    }
}
