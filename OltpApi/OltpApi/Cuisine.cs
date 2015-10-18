using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWriters.CandidateSamples.ChrisGrella.RestaurantReviews.ObjectModel
{
	public class Cuisine : PersistedObject<Cuisine>
	{
		public string Name { get; set; }

		/// <summary>
		/// Returns a list of all cuisines whose <see cref="Name"/> matches the given starting text, for use in type-ahead fields.
		/// </summary>
		/// <param name="prefix">The start of the string to match.</param>
		/// <returns></returns>
		public List<Cuisine> FindAllStartingWith(string prefix)
		{
			return FindAll().Where(c => c.Name.StartsWith(prefix)).ToList();
		}
	}
}
