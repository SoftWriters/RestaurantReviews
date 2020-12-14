using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.Logic.Model.Restaurant.Post
{
    public class PostRestaurantRequest : IEntityBuilder<Data.Restaurant>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [StringLength(2)]
        public string State { get; set; }
        [Required]
        [StringLength(9)]
        public string Zip { get; set; }

        public Data.Restaurant Build()
        {
            return new Data.Restaurant()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                City = City,
                State = State,
                ZipCode = Zip
            };
        }
    }
}
