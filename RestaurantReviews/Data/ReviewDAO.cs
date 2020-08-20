using RestaurantReviews.Api.Model;
using RestaurantReviews.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReviews.Data
{
    public class ReviewDAO
    {
        //
        // In place of a real DB, I setup this in-memory data structure to contain Review data
        //
        public static ConcurrentDictionary<string, Review> Reviews = new ConcurrentDictionary<string, Review>();
        public static ConcurrentDictionary<Guid, Review> ReviewsById = new ConcurrentDictionary<Guid, Review>();

        public static Review Add(Review review)
        {
            if (!Reviews.TryAdd(review.GetKey(), review))
                return null;

            review.ReviewId = Guid.NewGuid();
            ReviewsById.TryAdd(review.ReviewId, review);

            return review;
        }

        public static Review Add(DateTime reviewDate, Guid restaurantId, Guid userId, Rating rating, string comments)
        {
            Review review = new Review(reviewDate, restaurantId, userId, rating, comments);

            if (!Reviews.TryAdd(review.GetKey(), review))
                return null;

            review.ReviewId = Guid.NewGuid();
            ReviewsById.TryAdd(review.ReviewId, review);

            return review;
        }

        public static bool Delete(Review review)
        {
            Review deleted;
            Reviews.TryRemove(review.GetKey(), out deleted);

            if (deleted != null)
                ReviewsById.TryRemove(deleted.ReviewId, out deleted);

            return (deleted != null);
        }

        public static bool Delete(Guid reviewId)
        {
            Review deleted;
            ReviewsById.TryRemove(reviewId, out deleted);

            if (deleted != null)
                Reviews.TryRemove(deleted.GetKey(), out deleted);

            return (deleted != null);
        }

        public static List<Review> GetReviewsByUser(Guid userId)
        {
            List<Review> results = new List<Review>();

            foreach (Review review in Reviews.Values)
                if (review.UserId.Equals(userId))
                    results.Add(review);

            return results;
        }

        static ReviewDAO()
        {
            int reviewCount = 0;

            foreach (Restaurant restaurant in RestaurantDAO.Restaurants.Values)
                foreach (User user in UserDAO.Users.Values)
                    Add(DateTime.Now, restaurant.RestaurantId, user.UserId, Rating.ThreeStar, "Sample Review " + ++reviewCount);
        }
    }
}
