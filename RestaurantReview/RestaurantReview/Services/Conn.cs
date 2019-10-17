using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.Services
{
    public class Conn : IConn
    {
        public string AWSconnstring()
        {
            return @"Data Source=restaurantreviewsdb.cur7afppexfe.us-east-2.rds.amazonaws.com,1433; Initial Catalog=RestaurantReviewManager;User ID=admin;Password=Whatthe770!";
        }

        public string connstring()
        {
            return @"Data Source=DESKTOP-B54NHFS ; Initial Catalog=RestaurantReviewManager; Integrated Security=SSPI;";
        }
    }
}
