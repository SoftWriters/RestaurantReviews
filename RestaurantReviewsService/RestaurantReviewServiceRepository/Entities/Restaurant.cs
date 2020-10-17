using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewService.Entities
{
    public sealed class Restaurant : EntityModelBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int StateIdRef { get; set; }

        public string Overview { get; set; }

     
    }
}
