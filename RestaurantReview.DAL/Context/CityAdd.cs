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
        public int CityAdd(string City, string State)
        {
            SqlParameter cityParam = new SqlParameter("@City", City);
            SqlParameter stateParam = new SqlParameter("@State", State);
            int ret = this.Database.SqlQuery<int>("CityAdd @CityID, @State", cityParam, stateParam).Single();
             
            return ret;
        }
    }
}
