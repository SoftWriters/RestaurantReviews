using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReview.Service.Interface;
using Moq;
using RestaurantReview.DAL.Interface;
using RestaurantReview.DAL.Entity;
using System.Data.Entity;
using RestaurantReview.Service;

namespace RestaurantReview.Test.Base
{
    public abstract class RestaurantBase
    {
        protected IRestaurantService _service;
        protected Mock<IRRContext> _mockContext;
        protected Mock<DbSet<Restaurant>> _mockSet;
        protected IQueryable<Restaurant> listRestaurant;

        public void Initialize()
        {
            listRestaurant = new List<Restaurant>() {
          new Restaurant() { Id = 1, Name = "Apple Bees", CityID = 1 },
          new Restaurant() { Id = 2, Name = "TGI Fridays", CityID = 1 },
          new Restaurant() { Id = 3, Name = "Olive Garden", CityID = 2 }
         }.AsQueryable();

            _mockSet = new Mock<DbSet<Restaurant>>();
            _mockSet.As<IQueryable<Restaurant>>().Setup(m => m.Provider).Returns(listRestaurant.Provider);
            _mockSet.As<IQueryable<Restaurant>>().Setup(m => m.Expression).Returns(listRestaurant.Expression);
            _mockSet.As<IQueryable<Restaurant>>().Setup(m => m.ElementType).Returns(listRestaurant.ElementType);
            _mockSet.As<IQueryable<Restaurant>>().Setup(m => m.GetEnumerator()).Returns(listRestaurant.GetEnumerator());

            _mockContext = new Mock<IRRContext>();
            _mockContext.Setup(c => c.Set<Restaurant>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.Restaurants).Returns(_mockSet.Object);

            _service = new RestaurantService(_mockContext.Object);

        }
    }
}
