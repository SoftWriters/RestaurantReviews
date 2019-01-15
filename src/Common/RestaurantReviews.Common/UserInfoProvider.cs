namespace RestaurantReviews.Common
{
    public interface IUserInfoProvider
    {
        UserInfo GetCurrentUserInfo();
    }

    public class UserInfoProvider : IUserInfoProvider
    {
        UserInfo _userInfo;

        public UserInfoProvider(UserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        public UserInfo GetCurrentUserInfo()
        {
            return _userInfo;
        }
    }
}