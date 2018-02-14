using System;
using System.Linq;
using DM = RestaurantReviewsAPI.DataModels;

namespace RestaurantReviewsAPI.Models
{
	public class Restaurant
	{
		#region Properties

		public int Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public State State { get; set; }
		public User Creator { get; set; }
		private DM.User CreatorUserDb { get; set; }

		#endregion Properties

		#region GetQueryable

		internal static IQueryable<Restaurant> GetQueryable(DM.RestaurantReviewEntities db)
		{
			var users = User.GetQueryable(db);
			var states = State.GetQueryable(db);

			return from r in db.Restaurants
						 join s in states on r.StateId equals s.Id
						 join u in users on r.CreatorUserId equals u.Id
						 select new Restaurant
						 {
							 Id = r.Id,
							 Name = r.Name,
							 City = r.City,
							 State = s,
							 Creator = u
						 };
		}

		#endregion GetQueryable

		#region Save

		/// <summary>
		/// Save Model object to Db
		/// </summary>
		/// <param name="db"></param>
		/// <returns></returns>
		public DM.Restaurant Save(DM.RestaurantReviewEntities db)
		{
			if (!Valid(db)) return null;
			var restaurantDb = new DM.Restaurant
			{
				Name = Name,
				City = City,
				StateId = State.Id,
				CreatorUserId = CreatorUserDb.Id
			};
			db.Restaurants.Add(restaurantDb);
			return restaurantDb;
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
			if (string.IsNullOrEmpty(Creator.Username))
			{
				throw new Exception("Creator is required.");
			}

			CreatorUserDb = db.Users.FirstOrDefault(u => u.UserName == Creator.Username);
			if (CreatorUserDb == null)
			{
				throw new Exception("Creator was not found.");
			}

			if (string.IsNullOrEmpty(Name))
			{
				throw new Exception("Restaurant Name is required.");
			}

			if (string.IsNullOrEmpty(City))
			{
				throw new Exception("Restaurant City is required.");
			}

			if (State == null)
			{
				throw new Exception("Restaurant State is required.");
			}

			if (!db.States.Any(s => s.Id == State.Id))
			{
				throw new Exception("Restaurant State was not found.");
			}

			return true;
		}

		#endregion Valid
	}
}