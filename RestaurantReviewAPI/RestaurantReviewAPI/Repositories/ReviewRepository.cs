using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviewAPI.Repositories
{
	//This class has linq commands to get data(Reviews Table) from the database.
	public class ReviewRepository
	{

		private static RestaurantReviewEntities dataContext = new RestaurantReviewEntities();
		public static object GetAllReviews()
		{
			var query = from Review in dataContext.Reviews  join Restaurant  in dataContext.Restaurants  on Review.RestaurantId equals Restaurant.RestaurantId  select
						new {RestaurantName=Restaurant.Name, Comments = Review.Comment, Ratings = Review.Stars };
			return query;
		}

		public static object GetReviewByUser(int userId)
		{
			var query = from Review in dataContext.Reviews
						join Restaurant in dataContext.Restaurants on Review.RestaurantId equals Restaurant.RestaurantId where Review.UserId == userId
						select new {RevId = Review.Id ,RestaurantName = Restaurant.Name, Comments = Review.Comment, Ratings = Review.Stars };
			//from Review in dataContext.Reviews where Review.UserId == userId select Review;
			return query;
		}

		public static void InsertReview(Review rev)
		{
			dataContext.Reviews.Add(rev);
			dataContext.SaveChanges();
			

		}

		public static void DeleteReview(int id)
		{
			var query = (from Review in dataContext.Reviews
						 where Review.Id== id
						 select Review).SingleOrDefault();
			dataContext.Reviews.Remove(query);
			dataContext.SaveChanges();
			//return GetReviewByUser(rev.UserId);
		}
	}
}