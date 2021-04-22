using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface ICityRepository
    {
        City ReadCity(int id);
        IList<City> ReadAllCities();
        void CreateCity(City city);
    }
}
