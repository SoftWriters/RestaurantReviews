using System;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews
{
    public class Restaurant : Entity<long>
    {
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public virtual long? CityId { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public Restaurant()
        {
            CreationTime = DateTime.Now;
        }
    }
}
