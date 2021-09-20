using RestaurantReviews.DataAccess;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace RestaurantReviews.Controllers.Api
{
    public class UserController : ApiController
    {
        // POST api/User/Login
        [HttpPost]
        public int Login([FromBody]LoginModel model)
        {
            KeyValuePair<int, UserModel> loginResult = UserManager.UserLogin(model.Username, model.Password);
            if (loginResult.Key > 0)
            {
                SessionHelper.SetUser(loginResult.Value);
            }

            return loginResult.Key;
        }

        // PUT api/User
        [HttpPut]
        public int Put([FromBody]UserModel user)
        {
            // Check permissions
            UserModel sessionUser = SessionHelper.GetUser();
            if ((user.ID > 0) && (sessionUser.ID != user.ID))
            {
                // Wrong user!
                return -1;
            }

            int result = UserManager.UpdateUser(user);

            // Update session user information, if appropriate
            if (((user.ID > 0) && (sessionUser.ID == user.ID)) || (sessionUser.ID == 0))
            {
                if (user.ID == 0)
                    user.ID = result;

                SessionHelper.SetUser(user);
            }

            return result;
        }
    }
}