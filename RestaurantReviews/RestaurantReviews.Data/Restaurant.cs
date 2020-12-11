using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.Data
{
    public class Restaurant : NamedEntity
    {
        [StringLength(maximumLength: 50)]
        public string City { get; set; }
        [StringLength(maximumLength: 2)]
        public string State { get; set; }
        [StringLength(maximumLength: 9)]
        public string ZipCode { get; set; }

        public IList<Review> Reviews { get; set; }
    }
}