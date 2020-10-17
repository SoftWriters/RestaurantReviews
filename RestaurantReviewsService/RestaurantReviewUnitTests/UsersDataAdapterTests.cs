using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviewsService.DataAdapters;
using RestaurantReviewsService.DomainModels;

namespace RestaurantReviewUnitTests
{
    [TestClass]
    public class UsersDataAdapterTests : UnitTestBase
    {
        private const string _testCategory = "DataAdapterTests";

        public UsersDataAdapterTests() : base()
        {

        }

        [TestMethod]
        [TestCategory(_testCategory)]
        public void CanGetAllUsersDomainModelList()
        {
            try
            {
                IUsersDataAdapter usersDataAdapter = new UsersDataAdapter();

                IList<UserDM> results = usersDataAdapter.GetAllUsers();

                Assert.IsTrue(results != null);

                Assert.IsTrue(results.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        [TestCategory(_testCategory)]
        public void CanGetUserByName()
        {
            try
            {
                string firstName = "Dale";
                string lastName = "Cooper";

                IUsersDataAdapter usersDataAdapter = new UsersDataAdapter();

                UserDM results = usersDataAdapter.GetUserByName(firstName, lastName);

                Assert.IsTrue(results != null);

                Assert.IsTrue(results.FirstName == firstName);
                Assert.IsTrue(results.LastName == lastName);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
