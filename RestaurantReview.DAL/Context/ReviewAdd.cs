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
        public int ReviewAdd(int RestaurantID, int UserID, int Rating, string Comments)
        {
            SqlParameter restParam = new SqlParameter("@RestaurantID", RestaurantID);
            SqlParameter userParam = new SqlParameter("@UserID", UserID);
            SqlParameter ratingParam = new SqlParameter("@Rating", Rating);
            SqlParameter commentsParam = new SqlParameter("@Comments", Comments);
            int ret = this.Database.SqlQuery<int>("ReviewAdd @RestaurantID, @UserID, @Rating, @Comments",
                restParam, userParam, ratingParam, commentsParam).Single();

            return ret;
        }
    }
}
