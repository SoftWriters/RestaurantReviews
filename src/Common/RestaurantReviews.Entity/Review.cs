using RestaurantReviews.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Entity
{
    public class Review : IEntity
    {
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("restaurant_id")]
        public int RestaurantId { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}
