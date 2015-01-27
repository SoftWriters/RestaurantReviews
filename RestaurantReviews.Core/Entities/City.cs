using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReviews
{
    public class City : Entity<long>
    {
        [ForeignKey("StateId")]
        public virtual State State { get; set; }
        public virtual int? StateId { get; set; }

        public virtual string Name { get; set; }

        public virtual int ZipCode { get; set; }
    }
}
