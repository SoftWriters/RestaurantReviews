using System.Collections.Generic;

namespace Model
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public virtual List<Review> Reviews { get; set; }
    }
}