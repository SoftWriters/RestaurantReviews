using Abp.Domain.Entities;

namespace RestaurantReviews
{
    public class User : Entity <long>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Username { get; set; }
    }
}
