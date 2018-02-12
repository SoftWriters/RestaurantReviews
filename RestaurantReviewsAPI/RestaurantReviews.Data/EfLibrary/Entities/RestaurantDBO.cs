using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Data.EfLibrary.Entities
{
    public class RestaurantDBO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public StateDBO State { get; set; }
    }
}
