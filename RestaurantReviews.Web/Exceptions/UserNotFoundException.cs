using RestaurantReviews.Web.Data.Entities;

namespace RestaurantReviews.Web
{
    public class UserNotFoundException : NotFoundException<User>
    {
        public UserNotFoundException() { }

        public UserNotFoundException(int userId)
        {
            _message = $"No user found for ID { userId }";
        }
    }
}
