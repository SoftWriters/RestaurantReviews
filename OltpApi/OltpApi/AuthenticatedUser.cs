using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWriters.CandidateSamples.ChrisGrella.RestaurantReviews.ObjectModel
{
	/// <summary>
	/// Represents a user of our app, authenticated via manual account creation (email address, login) or Facebook pass-through.
	/// </summary>
	public class AuthenticatedUser : PersistedObject<AuthenticatedUser>
	{
		public string UserName { get; set; }

		public static AuthenticatedUser FindByUserName(string userName)
		{
			//TODO: Need persistence layer for lookup
			throw new NotImplementedException();
		}
	}
}
