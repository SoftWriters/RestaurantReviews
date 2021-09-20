using RestaurantReviews.Models;
using System.Collections.Generic;

namespace RestaurantReviews.DataAccess
{
    public static class ReviewManager
    {
        public static List<ReviewInfoModel> GetReviews(int ID, int userID, int restaurantID)
        {
            return DBCaller.CreateModelList<ReviewInfoModel>("proc_GetReview", DBCaller.CreateParameterList("@ID", ID, "@UserID", userID, "@RestaurantID", restaurantID));
        }

        public static ReviewInfoModel GetReview(int ID)
        {
            return GetReviews(ID, 0, 0)[0];
        }

        public static int InsertReview(int userID, int restaurantID, string title, string description)
        {
            return UpdateReview(0, userID, restaurantID, title, description);
        }

        public static int UpdateReview(ReviewModel review)
        {
            return UpdateReview(review.ID, review.UserID, review.RestaurantID, review.Title, review.Description);
        }

        public static int UpdateReview(int ID, int userID, int restaurantID, string title, string description)
        {
            return DBCaller.Call("proc_UpdateReview", DBCaller.CreateParameterList("@ID", ID, "@UserID", userID, "@RestaurantID", restaurantID, "@Title", title, "@Description", description));
        }

        public static int DeleteReview(int ID, int userID)
        {
            return DBCaller.Call("proc_DeleteReview", DBCaller.CreateParameterList("@ID", ID, "@UserID", userID));
        }

        public static ReviewViewModel GetReviewViewModel(int userID, int restaurantID)
        {
            return new ReviewViewModel
            {
                UserID = userID,
                RestaurantID = restaurantID,
                Reviews = GetReviews(0, userID, restaurantID),
                Users = UserManager.GetAllUsers(),
                Restaurants = RestaurantManager.GetAllRestaurants()
            };
        }

        public static ReviewUpdateViewModel GetReviewUpdateViewModel(int reviewID, int restaurantID = 0)
        {
            return new ReviewUpdateViewModel
            {
                Review = (reviewID == 0) ? new ReviewInfoModel() { RestaurantID = restaurantID } : GetReview(reviewID),
                Restaurants = RestaurantManager.GetAllRestaurants()
            };
        }
    }
}
