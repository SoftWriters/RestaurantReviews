using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantReviews.Data
{
    public interface IEntity
    {
        [Key]
        [Required]
        Guid Id { get; set; }
        [Required]
        DateTime DateCreated { get; set; }
        [Required]
        DateTime DateModified { get; set; }
    }

    public class Entity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
    }
}
