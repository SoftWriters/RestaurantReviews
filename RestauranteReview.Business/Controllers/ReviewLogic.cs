using RestaurantReview.BusinessLogic;
using RestaurantReview.BusinessLogic.Models;
using RestaurantReview.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace reviewReview.BusinessLogic.Controllers
{
    public class ReviewLogic : Logic
    {
        private ReviewDBContext _reviewDBContext;
        private RestaurantDBContext _restaurantDBContext;

        public ReviewLogic()
        {
            _reviewDBContext = new ReviewDBContext();
            _restaurantDBContext = new RestaurantDBContext();
        }

        public bool TryAddReview(ReviewContext reviewContext, out HttpStatusCode suggestedStatusCode, out string errorMessage, out Review result)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";
            result = null;

            try
            {
                // validate restaurant ID from review exists
                if (!_restaurantDBContext.Restaurants.Any(x => x.id == reviewContext.restaurantID))
                {
                    suggestedStatusCode = HttpStatusCode.BadRequest;
                    errorMessage = $"Invalid review, restaurant ID {reviewContext.restaurantID} does not exist. Please create a restaurant for this review first";
                    return false;
                }

                // add review context to database
                reviewContext = _reviewDBContext.Reviews.Add(reviewContext);
                _reviewDBContext.SaveChanges();

                result = new Review(reviewContext);
                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }

        public bool TryGetReview(int reviewID, out HttpStatusCode suggestedStatusCode, out string errorMessage, out Review result)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";
            result = null;

            try
            {
                ReviewContext reviewContext = _reviewDBContext.Reviews.Find(reviewID);

                // validate review record exists
                if (reviewContext != null)
                {
                    result = new Review(reviewContext);
                }

                else
                {
                    suggestedStatusCode = HttpStatusCode.BadRequest;
                    errorMessage = $"Invalid review ID: {reviewID}. Record not found.";
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }

        public bool TryGetReviews(out HttpStatusCode suggestedStatusCode, out string errorMessage, out List<Review> reviews)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";
            reviews = null;

            try
            {
                reviews = new List<Review>();

                // generate list of all review records
                foreach (ReviewContext reviewContext in _reviewDBContext.Reviews)
                {
                    ReviewContext review = reviewContext as ReviewContext;

                    if (review != null)
                    {
                        reviews.Add(new Review(review));
                    }

                    else
                    {
                        errorMessage += $"review record for {reviewContext.id} is corrupted. Unable to return.\r\n";
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }

        public bool TryUpdateReview(int reviewID, ReviewContext review, out HttpStatusCode suggestedStatusCode, out string errorMessage, out Review result)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";
            result = null;

            try
            {
                // find record from database
                ReviewContext reviewContext = _reviewDBContext.Reviews.Find(reviewID);
                if (reviewContext != null)
                {
                    reviewContext.rating = review.rating;
                    reviewContext.comments = review.comments;
                    _reviewDBContext.SaveChanges();
                    result = new Review(reviewContext);
                }

                else
                {
                    suggestedStatusCode = HttpStatusCode.BadRequest;
                    errorMessage = $"Invalid review ID: {review.id}. Record not found.";
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }

        public bool TryDeleteReview(int reviewID, out HttpStatusCode suggestedStatusCode, out string errorMessage)
        {
            suggestedStatusCode = HttpStatusCode.OK;
            errorMessage = "";

            try
            {
                // find record from database
                ReviewContext reviewContext = _reviewDBContext.Reviews.Find(reviewID);

                if (reviewContext != null)
                {
                    // delete record from database
                    _reviewDBContext.Reviews.Remove(reviewContext);
                    _reviewDBContext.SaveChanges();
                }

                else
                {
                    suggestedStatusCode = HttpStatusCode.BadRequest;
                    errorMessage = $"Invalid review ID: {reviewID}. Record not found.";
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                return TotalFailureMessage(ex, out suggestedStatusCode, out errorMessage);
            }
        }
    }
}
