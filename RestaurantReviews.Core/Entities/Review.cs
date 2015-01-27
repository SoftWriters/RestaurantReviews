using System;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews
{
    public class Review : Entity<long>
    {
        [ForeignKey("ReviewerId")]
        public virtual User Reviewer { get; set; }
        public virtual long? ReviewerId { get; set; }

        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        public virtual long? RestaurantId { get; set; }

        public virtual float Rating { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public Review()
        {
            CreationTime = DateTime.Now;
        }
    }
}
