using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EateryviewApi.Models
{
    public class Review
    {
        public long Id { get; set; }
        public long RestaurantId { get; set; }
        public string UserName { get; set; }
        public double Stars { get; set; }
        public string Description { get; set; }
    }
}
