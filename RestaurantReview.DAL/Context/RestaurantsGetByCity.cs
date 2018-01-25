using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReview.DAL.Entity;

namespace RestaurantReview.DAL.Context
{
    public partial class RR
    {
        public IEnumerable<Entity.Restaurant> RestaurantsGetByCity(int CityID)
        {
            SqlParameter cityParam = new SqlParameter("@CityID", CityID);
            List<Restaurant> rest = this.Database.SqlQuery<Restaurant>("RestaurantsGetByCity @CityID", cityParam).ToList();

            return rest;
        }
    }
}
