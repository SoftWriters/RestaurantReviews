using System;
using System.Linq;
using DM = RestaurantReviewsAPI.DataModels;

namespace RestaurantReviewsAPI.Models
{
	public class Review
	{
		#region Properties

		public int Id { get; set; }
		public string ReviewText { get; set; }
		public Restaurant Restaurant { get; set; }
		public User Reviewer { get; set; }
		private DM.User ReviewerUserDb { get; set; }

		#endregion Properties

		#region GetQueryable

		internal static IQueryable<Review> GetQueryable(DM.RestaurantReviewEntities db)
		{
			var restaurants = Restaurant.GetQueryable(db);
			var users = User.GetQueryable(db);

			return from rv in db.Reviews
						 join r in restaurants on rv.RestaurantId equals r.Id
						 join u in users on rv.ReviewerUserId equals u.Id
						 select new Review
						 {
							 Id = r.Id,
							 ReviewText = rv.ReviewText,
							 Restaurant = r,
							 Reviewer = u
						 };
		}

		#endregion GetQueryable

		#region Save

		/// <summary>
		/// Save Model object to Db
		/// </summary>
		/// <param name="db"></param>
		/// <returns></returns>
		public DM.Review Save(DM.RestaurantReviewEntities db)
		{
			if (!Valid(db)) return null;
			var reviewDb = new DM.Review
			{
				RestaurantId = Restaurant.Id,
				ReviewText = ReviewText,
				ReviewerUserId = ReviewerUserDb.Id
			};
			db.Reviews.Add(reviewDb);
			return reviewDb;
		}

		#endregion Save

		#region Valid

		/// <summary>
		/// Validate model object before save
		/// </summary>
		/// <param name="creatorUserDb"></param>
		/// <returns></returns>
		private bool Valid(DM.RestaurantReviewEntities db)
		{
			if (string.IsNullOrEmpty(Reviewer.Username))
			{
				throw new Exception("Reviewer is required.");
			}

			ReviewerUserDb = db.Users.FirstOrDefault(u => u.UserName == Reviewer.Username);
			if (ReviewerUserDb == null)
			{
				throw new Exception("Reviewer was not found.");
			}

			if (Restaurant == null)
			{
				throw new Exception("Restaurant is required.");
			}

			if (!db.Restaurants.Any(r => r.Id == Restaurant.Id))
			{
				throw new Exception("Restaurant was not found.");
			}

			if (string.IsNullOrEmpty(ReviewText))
			{
				throw new Exception("Review is required.");
			}

			return true;
		}

		#endregion Valid
	}
}