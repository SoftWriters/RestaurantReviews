using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EateryviewApi.Models
{
    public class Restaurant
    {
        public long Id { get;  set; }
        public string Name { get; set; }
        public string StreetAddr { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Description { get; set; }
        public double AvgStars { get; set; }
    }
}
