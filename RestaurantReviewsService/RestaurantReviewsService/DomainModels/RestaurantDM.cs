using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviewsService.DomainModels
{
    public sealed class RestaurantDM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int StateId { get; set; }

        public string Overview { get; set; }
    }
}