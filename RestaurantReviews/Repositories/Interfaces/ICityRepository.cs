using Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface ICityRepository
    {
        bool HasData();

        IEnumerable<ICityModel> AddCity(ICityModel city);

        ICityModel GetCityById(int id);
    }
}
