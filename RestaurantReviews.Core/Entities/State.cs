using Abp.Domain.Entities;

namespace RestaurantReviews
{
    public class State : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Abbr { get; set; }
    }
}
