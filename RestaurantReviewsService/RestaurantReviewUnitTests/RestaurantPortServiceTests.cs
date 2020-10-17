using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviewsService.PortServices;
using RestaurantReviewsService.ViewModels;

namespace RestaurantReviewUnitTests
{
    [TestClass]
    public class RestaurantPortServiceTests : UnitTestBase
    {
        private const string _testCategory = "PortServiceTests";

        public RestaurantPortServiceTests() : base()
        {
        }

        [TestMethod]
        [TestCategory(_testCategory)]
        public void CanAddNewRestaurantReview()
        {
            const int RestIdLaPaloma = 2;
            const int UserIdMTWence = 1;
            const int RatingsIdVeryGood = 3;
            string title = "Great food kind of small";
            string comments = "The number 4 combination platter was fantastic";

            try
            {

                IRestaurantPortService restaurantPortService = new RestaurantPortService();

                restaurantPortService.PostRestaurantReview(
                    new UserReviewViewModel
                    {
                        RatingsRef = RatingsIdVeryGood,
                        UserIdRef = UserIdMTWence,
                        Title = title,
                        Comments = comments,
                        RestaurantIdRef = RestIdLaPaloma
                    });

                Assert.IsTrue(true);
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
