using Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class RRContext : DbContext
    {
        public DbSet<Restaurant> Resturants { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }
    }

    public class RestaurantReviewView
    {
        public string RestaurantName { get; set; }

        public string ReviewerName { get; set; }

        public string ReviewText { get; set; }
    }

    public class Repository
    {
        public List<string> GetResaurantsByCity(string city)
        {
            using (var db = new RRContext())
            {
                var query = from r in db.Resturants
                            where r.City == city
                            orderby r.Name
                            select r.Name;

                return query.ToList();
            }
        }

        public void SaveRestaurant(Restaurant restaurant)
        {
            using (var db = new RRContext())
            {
                db.Resturants.Add(restaurant);
                db.SaveChanges();
            }
        }

        // find restaurant. create a review save.
        public void SaveReview(Restaurant restaurant, string reviewText, User user)
        {
            using (var db = new RRContext())
            {
                db.Reviews.Add(new Review() { Restaurant = restaurant, ReviewText = reviewText, User = user });

                db.SaveChanges();
            }
        }

        public List<RestaurantReviewView> GetReviewsByUser(int userid)
        {
            using (var db = new RRContext())
            {
                db.Resturants.Load();
                db.Users.Load();
                User user = db.Users.Find(userid);

                var queryReviews = user.Reviews.Select(x => new RestaurantReviewView
                {
                    RestaurantName = x.Restaurant.Name,
                    ReviewerName = x.User.Name,
                    ReviewText = x.ReviewText
                });

                return queryReviews.ToList();
            }
        }

        public void DeleteReview(int ReviewID)
        {
            using (var db = new RRContext())
            {
                Review review = db.Reviews.Find(ReviewID);
                db.Reviews.Remove(review);
                db.SaveChanges();
            }
        }
    }
}

