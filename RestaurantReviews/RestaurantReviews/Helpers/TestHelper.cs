using RestaurantReviews.Models;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace RestaurantReviews.Helpers
{
    public static class TestHelper
    {
        private static UserModel _testUser = null;
        public static UserModel TestUser
        {
            get
            {
                if (_testUser == null)
                    _testUser = GetTestUser();

                return _testUser;
            }
        }

        public static HttpContext TestHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 10, true,
                HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);

            SessionStateUtility.AddHttpSessionStateToContext(httpContext, sessionContainer);

            return httpContext;
        }

        // Avoids redirect for tests on pages that require logged in user
        private static UserModel GetTestUser()
        {
            return new UserModel() { ID = 1, FirstName = "Test", LastName = "User", Username = "tuser", Password = "tuser_pass" };
        }
    }
}