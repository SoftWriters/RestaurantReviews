
namespace RestaurantReviews
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class User : UserDTO
    {
        public long UserID { get; set; }
    }
}
