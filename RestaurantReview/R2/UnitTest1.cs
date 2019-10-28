using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReview.Controllers;
using RestaurantReview.Models;
using RestaurantReview.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using RestaurantReview.DAL;
using System.Data.SqlClient;


namespace RestaurantReviewMSTests.Controllers
{
    [TestClass]
    public class Restaurants
    {
        public IConn connection = new Conn();

        [TestMethod]
        public void RestaurantsControllerTest()
        {
            var RC = new RestaurantsController(connection);
            var result = RC.Get("Boston");
            var resultType = result as ActionResult<List<Restaurant>>;
            Assert.IsInstanceOfType(resultType, typeof(ActionResult<List<Restaurant>>));
        }
    }
}
