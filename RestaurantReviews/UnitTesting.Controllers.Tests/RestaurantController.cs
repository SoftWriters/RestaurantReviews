using System;
using System.Collections.Generic;
using NUnit.Framework;
using Models;
using Moq;
using Repositories;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;

namespace UnitTesting.Controllers.Tests
{
    // Notes:
    // 
    // I am not using mock objects for the Models, because they are all standard C# objects.  If 
    // they don't work, there are larger problems that will prevent these tests from running in 
    // the first place.  Technically that makes these more Integration Tests, but I think I can
    // get away with calling them Unit Tests.
    //
    // Possible changes:
    //
    // Do the complete chain (i.e. change the *Controller.Request values to be actual URIs) instead of
    //        calling just the method on the controller.
    [TestFixture]
    class RestaurantController
    {
        List<IChainModel> _chains;
        List<ICityModel> _cities;
        List<IRestaurantModel> _restaurants;
        List<IReviewModel> _reviews;
        List<IUserModel> _users;
        
        [OneTimeSetUp]
        public void Initialize()
        {
            _chains = new List<IChainModel>();
            _cities = new List<ICityModel>();
            _restaurants = new List<IRestaurantModel>();
            _reviews = new List<IReviewModel>();
            _users = new List<IUserModel>();

            var mon = new CityModel("Monroeville, PA");
            var oak = new CityModel("Oakland, PA");
            var mcc = new CityModel("McCandless, PA");
            var asp = new CityModel("Aspinwall, PA");
            var pit = new CityModel("Pittsburgh, PA");
            var rob = new CityModel("Robinson Township, PA");

            var og = new ChainModel("Olive Garden");
            var mm = new ChainModel("Mad Mex");

            var mmoak = new RestaurantModel("Mad Mex", oak, mm, "Atwood Street");
            var mmmcc = new RestaurantModel("Mad Mex", mcc, mm, "McKnight Road");
            var mmmon = new RestaurantModel("Mad Mex", mon, mm, "Miracle Mile");
            var ogmon = new RestaurantModel("Olive Garden", mon, og, "Monroeville Mall");
            var ogmcc = new RestaurantModel("Olive Garden", mcc, og, "Ross Park Mall");
            var ogrob = new RestaurantModel("Olive Garden", rob, og, "Fayette Center");
            var corner = new RestaurantModel("Cornerstone", asp, null, "Freeport Road");
            var donp = new RestaurantModel("Don Parmesan's", mcc, null, "McKnight Road");

            var wbf = new UserModel("Bill Fisher", true);
            var joe = new UserModel("Joe Smith", false);

            var rev1 = new ReviewModel(wbf, mmoak, 4, 5, 4, 4, 5, "Eat here all the time");
            var rev2 = new ReviewModel(wbf, donp, 1, 1, 1, 1, 1, "Went out of business!");
            var rev3 = new ReviewModel(joe, mmoak, 4, 4, 4, 4, 4, "It's not truly Mexican food.");

            _chains.Add(og);
            _chains.Add(mm);

            _cities.Add(mon);
            _cities.Add(oak);
            _cities.Add(mcc);
            _cities.Add(asp);
            _cities.Add(pit);
            _cities.Add(rob);

            _restaurants.Add(mmoak);
            _restaurants.Add(mmmcc);
            _restaurants.Add(mmmon);
            _restaurants.Add(ogmon);
            _restaurants.Add(ogmcc);
            _restaurants.Add(ogrob);
            _restaurants.Add(corner);
            _restaurants.Add(donp);

            _reviews.Add(rev1);
            _reviews.Add(rev2);
            _reviews.Add(rev3);

            _users.Add(wbf);
            _users.Add(joe);
        }

        [Test]
        public void TestGetRestaurants()
        {
            // Alternate solution: Unity container for DI.
            var chainMock = new Mock<IChainRepository>();
            var cityMock = new Mock<ICityRepository>();
            var restaurantMock = new Mock<IRestaurantRepository>(MockBehavior.Strict);
            restaurantMock.Setup(repo => repo.GetRestaurants()).Returns(_restaurants);
            var reviewMock = new Mock<IReviewRepository>();
            var userMock = new Mock<IUserRepository>();

            RestaurantReview2.Controllers.RestaurantController restController = new RestaurantReview2.Controllers.RestaurantController(chainMock.Object, cityMock.Object, restaurantMock.Object, reviewMock.Object, userMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var actionResult = restController.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(contentResult);
            restaurantMock.Verify(repo => repo.GetRestaurants(), Times.Once);
            List<RestaurantModel> decoded = JsonConvert.DeserializeObject<List<RestaurantModel>>(contentResult.Content);
            
            Assert.IsNotNull(decoded);
            Assert.That(decoded, Has.Count.EqualTo(8));
            var first = decoded[0] as IRestaurantModel;
            Assert.That(first.Name, Is.EqualTo("Mad Mex"));
            Assert.That(first.City.Name, Is.EqualTo("Oakland, PA"));
            Assert.That(first.Chain.Name, Is.EqualTo("Mad Mex"));
            // TODO: test all records.
        }

        [Test]
        public void TestGetRestaurantById()
        {
            // Alternate solution: Unity container for DI.
            var chainMock = new Mock<IChainRepository>();
            var cityMock = new Mock<ICityRepository>();
            var restaurantMock = new Mock<IRestaurantRepository>(MockBehavior.Strict);
            restaurantMock.Setup(repo => repo.GetRestaurantById(It.IsAny<int>())).Returns(_restaurants[1]);
            var reviewMock = new Mock<IReviewRepository>();
            var userMock = new Mock<IUserRepository>();

            RestaurantReview2.Controllers.RestaurantController restController = new RestaurantReview2.Controllers.RestaurantController(chainMock.Object, cityMock.Object, restaurantMock.Object, reviewMock.Object, userMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var actionResult = restController.GetRestaurant(1);
            var contentResult = actionResult as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(contentResult);
            restaurantMock.Verify(repo => repo.GetRestaurantById(1), Times.Once);
            RestaurantModel decoded = JsonConvert.DeserializeObject<RestaurantModel>(contentResult.Content);

            Assert.IsNotNull(decoded);
            var first = decoded;
            Assert.That(first.Name, Is.EqualTo("Mad Mex"));
            Assert.That(first.City.Name, Is.EqualTo("McCandless, PA"));
            Assert.That(first.Chain.Name, Is.EqualTo("Mad Mex"));
            // TODO: other fields
        }

        [Test]
        public void TestAddRestaurant()
        {
            // Alternate solution: Unity container for DI.
            var chainMock = new Mock<IChainRepository>();
            var cityMock = new Mock<ICityRepository>();
            var restaurantMock = new Mock<IRestaurantRepository>(MockBehavior.Strict);
            restaurantMock.Setup(repo => repo.AddRestaurant(It.IsAny<IRestaurantModel>())).Returns(_restaurants);
            restaurantMock.Setup(repo => repo.GetRestaurants()).Returns(_restaurants);
            var reviewMock = new Mock<IReviewRepository>();
            var userMock = new Mock<IUserRepository>();

            RestaurantModel newRest = new RestaurantModel("Test", new CityModel("McCandless, PA"), null, "Test" );
            var json = JsonConvert.SerializeObject(newRest);
            //construct content to send
            var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost/addrestaurant"),
                Content = content
            };

            RestaurantReview2.Controllers.RestaurantController restController = new RestaurantReview2.Controllers.RestaurantController(chainMock.Object, cityMock.Object, restaurantMock.Object, reviewMock.Object, userMock.Object)
            {   
                Request = request,
                Configuration = new HttpConfiguration()
            };

            restController.PostRestaurant(request); // Ignoring response for now.

            restaurantMock.Verify(repo => repo.AddRestaurant(It.IsAny<IRestaurantModel>()), Times.Once);
        }
    }
}