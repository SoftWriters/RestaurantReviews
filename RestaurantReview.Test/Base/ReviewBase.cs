using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantReview.Service.Interface;
using Moq;
using RestaurantReview.DAL.Interface;
using RestaurantReview.DAL.Entity;
using System.Data.Entity;
using RestaurantReview.Service;

namespace RestaurantReview.Test.Base
{
    public abstract class ReviewBase
    {
        protected IReviewService _service;
        protected Mock<IRRContext> _mockContext;
        protected Mock<DbSet<Review>> _mockSet;
        protected IQueryable<Review> listReview;

        public void Initialize()
        {
            listReview = new List<Review>() {
            new Review() { Id = 1, RestaurantID = 0, UserID = 0, Rating = 1, Comments = "test", DateCreated = DateTime.Now },
            new Review() { Id = 2, RestaurantID = 0, UserID = 0, Rating = 1, Comments = "test", DateCreated = DateTime.Now },
            new Review() { Id = 3, RestaurantID = 0, UserID = 0, Rating = 1, Comments = "test", DateCreated = DateTime.Now },
         }.AsQueryable();

            _mockSet = new Mock<DbSet<Review>>();
            _mockSet.As<IQueryable<Review>>().Setup(m => m.Provider).Returns(listReview.Provider);
            _mockSet.As<IQueryable<Review>>().Setup(m => m.Expression).Returns(listReview.Expression);
            _mockSet.As<IQueryable<Review>>().Setup(m => m.ElementType).Returns(listReview.ElementType);
            _mockSet.As<IQueryable<Review>>().Setup(m => m.GetEnumerator()).Returns(listReview.GetEnumerator());

            _mockContext = new Mock<IRRContext>();
            _mockContext.Setup(c => c.Set<Review>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.Reviews).Returns(_mockSet.Object);

            _service = new ReviewService(_mockContext.Object);

        }
    }
}
