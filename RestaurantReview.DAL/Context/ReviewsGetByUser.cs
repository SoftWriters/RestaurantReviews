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
        public IEnumerable<Entity.Review> ReviewsGetByUser(int UserID)
        {
            SqlParameter userParam = new SqlParameter("@UserID", UserID);
            List<Review> rest = this.Database.SqlQuery<Review>("ReviewsGetByUser @UserID", userParam).ToList();

            return rest;
        }
    }
}
