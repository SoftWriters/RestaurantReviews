using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWriters.CandidateSamples.ChrisGrella.RestaurantReviews.ObjectModel
{
	/// <summary>
	/// Represents the hours of operation of a restaurant on a particular day of the week.
	/// </summary>
	public class DoWSpecificHoursOfOperation : PersistedObject<DoWSpecificHoursOfOperation>
	{
		public DayOfWeek DayOfWeek { get; set; }
		public DateTime OpenUtc { get; set; }
		public DateTime CloseUtc { get; set; }

		public bool IsOpenNow()
		{
			// Only snapshot it once
			DateTime now = DateTime.UtcNow;

			return
				DayOfWeek == now.DayOfWeek &&
				OpenUtc.TimeOfDay <= now.TimeOfDay &&
				CloseUtc.TimeOfDay > now.TimeOfDay; //TODO: I don't think this will work for restaurants that close after midnight
		}

		/// <summary>
		/// Renders this instance as a string in the format "{dayofweek} {opentime} - {closetime}".
		/// </summary>
		/// <param name="localTimeZone">Time zone of the person using the app.</param>
		/// <returns></returns>
		public string ToDisplayString(TimeZone localTimeZone)
		{
			return string.Format("{0} {1} - {2}",
				DayOfWeek,
				localTimeZone.ToLocalTime(OpenUtc),
				localTimeZone.ToLocalTime(CloseUtc));
		}
	}
}
