using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviews.Web.Data;
using System.Linq;

namespace RestaurantReviews.Web.Tests
{
    [Ignore]
    [TestClass]
    public class RestaurantReviewsContextTests
    {
        private IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private RestaurantReviewsContext _context;
        private DbContextOptionsBuilder<RestaurantReviewsContext> _builder = new DbContextOptionsBuilder<RestaurantReviewsContext>();

        public RestaurantReviewsContextTests()
        {
            var connectionString = _config.GetConnectionString("RestaurantReviewsDatabase");
            _builder.UseSqlServer(connectionString);
            _context = new RestaurantReviewsContext(_builder.Options);
        }

        [TestMethod]
        public void Users()
        {
            var users = _context.Users.Include(u => u.Reviews).ThenInclude(r => r.Restaurant).ToList();
        }

        [TestMethod]
        public void Restaurants()
        {
            var restaurants = _context.Restaurants.ToList();
        }

        [TestMethod]
        public void Reviews()
        {
            var reviews = _context.Reviews.Include(r => r.Restaurant).ToList();
        }
    }
}
