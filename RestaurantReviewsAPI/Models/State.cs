using System.Linq;
using DM = RestaurantReviewsAPI.DataModels;

namespace RestaurantReviewsAPI.Models
{
	public class State
	{
		#region Properties

		public int Id { get; set; }
		public string Name { get; set; }
		public string Abbreviation { get; set; }

		#endregion Properties

		#region GetQueryable

		internal static IQueryable<State> GetQueryable(DM.RestaurantReviewEntities db)
		{
			return from s in db.States
						 select new State
						 {
							 Id = s.Id,
							 Name = s.Name,
							 Abbreviation = s.Abbreviation
						 };
		}

		#endregion GetQueryable
	}
}