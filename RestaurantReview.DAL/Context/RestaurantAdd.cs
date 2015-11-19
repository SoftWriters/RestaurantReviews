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
        public int RestaurantAdd(int CityID, string Name)
        {
            SqlParameter cityParam = new SqlParameter("@CityID", CityID);
            SqlParameter nameParam = new SqlParameter("@Name", Name);
            int ret = this.Database.SqlQuery<int>("RestaurantAdd @CityID, @Name", cityParam, nameParam).Single();

            return ret;
        }
    }
}
