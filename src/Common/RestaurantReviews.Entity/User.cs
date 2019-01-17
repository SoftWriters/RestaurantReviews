using RestaurantReviews.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entity
{
    public class User : IEntity
    {
        [Range(0,0)]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }
    }
}
