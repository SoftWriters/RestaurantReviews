using Microsoft.AspNetCore.Mvc;
using RestaurantReview.Controllers;
using RestaurantReview.DAL;
using RestaurantReview.Models;
using RestaurantReview.Services;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace RestaurantReviewTests.DALTests
{
    public class RestaurantsDALTests
    {

        [Fact]
        public void PostRestaurant_AddsNewRestaurant()
        {
            var restaurant = new Restaurant
            {
                City = "Boston",
                Name = "The ultimate test restaurant"
            };

            var dal = new RestaurantsDAL(new Conn().AWSconnstring());
            var listcount = dal.GetRestaurants().Count;
            var post = dal.PostRestaurant(restaurant);
            var listCountAfterPost = dal.GetRestaurants().Count;
            Assert.True(listCountAfterPost == listcount + 1);
        }

        [Fact]
        public void GetRestaurants_ReturnsList()
        {
            var dal = new RestaurantsDAL(new Conn().AWSconnstring());
            var list = dal.GetRestaurants();

            Assert.True(list.Count > 1);
        }
    }
}
