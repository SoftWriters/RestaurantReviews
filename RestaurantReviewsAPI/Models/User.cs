using System.Linq;
using DM = RestaurantReviewsAPI.DataModels;

namespace RestaurantReviewsAPI.Models
{
	public class User
	{
		#region Properties

		public int Id { get; set; }
		public string Username { get; set; }
		public string EmailAddress { get; set; }

		#endregion Properties

		#region GetQueryable

		internal static IQueryable<User> GetQueryable(DM.RestaurantReviewEntities db)
		{
			return from u in db.Users
						 select new User
						 {
							 Id = u.Id,
							 Username = u.UserName,
							 EmailAddress = u.EmailAddress
						 };
		}

		#endregion GetQueryable
	}
}