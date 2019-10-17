using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReview.Services
{
    public class Conn : IConn
    {
       public string connstring()
        {
            return @"Data Source=DESKTOP-B54NHFS ; Initial Catalog=RestaurantReviewManager; Integrated Security=SSPI;";
        }
    }
}
