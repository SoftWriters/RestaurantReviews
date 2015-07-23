using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsAPI
{
    class Review
    {
        public int reviewID { get; set; }
        private Restaurant restaurant;
        private User user;
        public string reviewText { get; set; }
        public int rating { get; set; }
        public DateTime date;
        public bool disposed { get; set; }

        public Review(Restaurant restaurant, User user, string reviewText, int rating, DateTime date)
        {
            // reviewID would be incremented in the database
            this.restaurant = restaurant;
            this.user = user;
            this.reviewText = reviewText;
            this.rating = rating;
            this.date = date;
            this.disposed = false;
        }
        
        // This is a placeholder for the unknown data source of the larger system
        List<Review> reviews = new List<Review>();

        // Post a review for a restaurant
        public void PostReview(Restaurant restaurant, User user, string reviewText, int rating)
        {
            Review review = new Review(restaurant, user, reviewText, rating, DateTime.Now);
            reviews.Add(review);
        }

        // Return all Reviews for a given User
        public List<Review> GetReviewsByUser(User user)
        {
            List<Review> matchedReviews = new List<Review>();
            foreach (Review rev in reviews)
            {
                if (user.userID == rev.user.userID)
                {
                    matchedReviews.Add(rev);
                }
            }
            return matchedReviews;
        }

        // Delete a review - Typically I do not completely delete anything from the database but rather set a bit field to false (Review.disposed)
        public void DeleteReview(Review review)
        {
            foreach (Review rev in reviews)
            {
                if (review.reviewID == rev.reviewID)
                {
                    review.disposed = true;
                }
            }
        }
    }

}
