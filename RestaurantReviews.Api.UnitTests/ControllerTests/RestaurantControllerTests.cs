using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantReviews.Api.Controllers;
using RestaurantReviews.Api.DataAccess;
using RestaurantReviews.Api.Models;
using Xunit;

namespace RestaurantReviews.Api.UnitTests.ControllerTests
{
    public class RestaurantControllerTests
    {
        private static readonly Restaurant McDonalds = new Restaurant
        {
            Id = 123,
            Name = "McDonald's",
            Description = "The Golden Arches",
            City = "Pittsburgh",
            State = "PA"
        };
        private static readonly Restaurant Wendys = new Restaurant
        {
            Id = 456,
            Name = "Wendy's",
            Description = "Dave's Place",
            City = "Pittsburgh",
            State = "PA"
        };
        
        
        [Fact]
        public async Task GetListWithNoParamsReturnsList()
        {
            var mockRestaurantQuery = new Mock<IRestaurantQuery>();
            mockRestaurantQuery.Setup(q => q.GetRestaurants(null, null))
                .Returns(Task.FromResult(new List<Restaurant> {new Restaurant()}));
            var controller = new RestaurantController(mockRestaurantQuery.Object, null);

            var result = await controller.GetListAsync();

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<Restaurant>>(((OkObjectResult)result.Result).Value);
            var resultList = (List<Restaurant>)((OkObjectResult) result.Result).Value;
            Assert.Single(resultList);
        }
        
        [Fact]
        public async Task GetListWithNoCityReturnsBadRequest()
        {
            var controller = new RestaurantController(null, null);

            var result = await controller.GetListAsync(null, "PA");

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        
        [Fact]
        public async Task GetListWithNoStateReturnsBadRequest()
        {
            var controller = new RestaurantController(null, null);

            var result = await controller.GetListAsync("Pittsburgh", null);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetListWithParamsReturnsList()
        {
            var mockRestaurantQuery = new Mock<IRestaurantQuery>();
            mockRestaurantQuery.Setup(q => 
                    q.GetRestaurants(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new List<Restaurant> { McDonalds }));
            var controller = new RestaurantController(mockRestaurantQuery.Object, null);

            var result = await controller.GetListAsync("Pittsburgh", "PA");

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<Restaurant>>(((OkObjectResult)result.Result).Value);
            var resultList = (List<Restaurant>)((OkObjectResult) result.Result).Value;
            Assert.Single(resultList);
        }

        [Fact]
        public async Task GetSingleWithValidIdReturnsRestaurant()
        {
            var mockRestaurantQuery = new Mock<IRestaurantQuery>();
            mockRestaurantQuery.Setup(q => q.GetRestaurant(McDonalds.Id))
                .Returns(Task.FromResult(McDonalds));
            var controller = new RestaurantController(mockRestaurantQuery.Object, null);

            var result = await controller.GetAsync(McDonalds.Id);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<Restaurant>(((OkObjectResult)result.Result).Value);
            var restaurant = (Restaurant)((OkObjectResult) result.Result).Value;
            Assert.Equal(McDonalds.Name, restaurant.Name);
        }
        
        [Fact]
        public async Task GetSingleWithNonexistentIdReturnsNotFound()
        {
            var mockRestaurantQuery = new Mock<IRestaurantQuery>();
            mockRestaurantQuery.Setup(q => q.GetRestaurant(McDonalds.Id))
                .Returns(Task.FromResult(McDonalds));
            var controller = new RestaurantController(mockRestaurantQuery.Object, null);

            var result = await controller.GetAsync(Wendys.Id);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetSingleWithInvalidIdReturnsBadRequest()
        {
            var controller = new RestaurantController(null, null);

            var result = await controller.GetAsync(0);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        
        [Fact]
        public async Task PostNewRestaurantThatDoesNotExistReturnsCreated()
        {
            var mockRestaurantQuery = new Mock<IRestaurantQuery>();
            mockRestaurantQuery.Setup(q => q.GetRestaurant(McDonalds.Name, 
                    McDonalds.City, McDonalds.State))
                .Returns(Task.FromResult(McDonalds));
            var mockInsertRestaurant = new Mock<IInsertRestaurant>();
            var controller = new RestaurantController(mockRestaurantQuery.Object, 
                mockInsertRestaurant.Object);

            var result = await controller.PostAsync(Wendys);

            Assert.IsType<CreatedResult>(result.Result);
            mockInsertRestaurant.Verify(i => i.Insert(It.IsAny<NewRestaurant>()),
                Times.Once);
        }
        
        [Fact]
        public async Task PostNewRestaurantThatExistsReturnsConflict()
        {
            var mockRestaurantQuery = new Mock<IRestaurantQuery>();
            mockRestaurantQuery.Setup(q => q.GetRestaurant(McDonalds.Name, 
                    McDonalds.City, McDonalds.State))
                .Returns(Task.FromResult(McDonalds));
            var mockInsertRestaurant = new Mock<IInsertRestaurant>();
            var controller = new RestaurantController(mockRestaurantQuery.Object, 
                mockInsertRestaurant.Object);

            var result = await controller.PostAsync(McDonalds);

            Assert.IsType<ConflictObjectResult>(result.Result);
        }
    }
}