using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviewsService.DataAdapters;
using RestaurantReviewsService.DomainModels;


namespace RestaurantReviewUnitTests
{
    [TestClass]
    public class UserReviewsDataAdapterTests : UnitTestBase
    {
        private const string _testCategory = "DataAdapterTests";

        public UserReviewsDataAdapterTests() : base()
        {
        }

        [TestMethod]
        [TestCategory(_testCategory)]
        public void CanGetAllReviewsForAUser()
        {
            try
            {
                string userFirstName = "Dale";
                string userLastName = "Cooper";

                UserDM testUserDM = GetUser(userFirstName, userLastName);

                if(testUserDM == null)
                {
                    Assert.Fail("Could not get Test User needed for this test...");
                }

                IUserReviewsDataAdapter userReviewsDataAdapter = new UserReviewsDataAdapter();

                IList<UserReviewDM> results = userReviewsDataAdapter.GetUserReviewsForUser(testUserDM);

                Assert.IsTrue(results != null);

                Assert.IsTrue(results.Count > 0);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        private UserDM GetUser(string firstName, string lastName)
        {
            IUsersDataAdapter usersDataAdapter = new UsersDataAdapter();

            return usersDataAdapter.GetUserByName(firstName, lastName);
        }
    }
}
