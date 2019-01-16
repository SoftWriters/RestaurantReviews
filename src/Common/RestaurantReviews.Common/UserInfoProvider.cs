using System.Web;

namespace RestaurantReviews.Common
{
    public interface IUserInfoProvider
    {
        UserInfo GetCurrentUserInfo();
    }

    public class UserInfoProvider : IUserInfoProvider
    {
        public UserInfo GetCurrentUserInfo()
        {
            int.TryParse(HttpContext.Current.User.Identity.Name, out int id);
            return new UserInfo() { Id =  id};
        }
    }
}