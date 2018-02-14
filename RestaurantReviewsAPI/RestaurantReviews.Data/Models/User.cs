namespace RestaurantReviews.Data.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool ValidatePassword(string password)
        {
            return password == Password;
        }
    }
}
