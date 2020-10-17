using RestaurantReviewServiceRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewUnitTests
{
    public class TestSetup
    {
        public TestSetup()
        {
            
        }

        internal void Initialize()
        {
            Resources.SqlLiteDataBase = string.Format("{0}\\App_Data\\{1}",
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "RestaurantReviews.db");


        }
    }
}
