using RestaurantReviews.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entity
{
    public class Restaurant : IEntity
    {
        [Range(0,0)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
    }
}
