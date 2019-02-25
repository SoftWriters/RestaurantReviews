using RestaurantReviews.Data.Entities;

namespace RestaurantReviews.Data.Extensions
{
    public static class UserExtensions
    {
        public static void Map(this User dbUser, User user)
        {
            dbUser.EmailAddress = user.EmailAddress;
            dbUser.FirstName = user.FirstName;
            dbUser.IsActive = user.IsActive;
            dbUser.LastName = user.LastName;
            dbUser.MiddleName = user.MiddleName;
        }
    }
}