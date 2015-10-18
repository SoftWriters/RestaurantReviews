using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWriters.CandidateSamples.ChrisGrella.RestaurantReviews.ObjectModel
{
	/// <summary>
	/// Represents a Geocoded position, for the purpose of calculating distances and proximities.
	/// </summary>
	public class GeoPosition : PersistedObject<GeoPosition>
	{
		public double LongDecDegrees { get; set; }
		public double LatDecDegrees { get; set; }

		public double GetDistanceInMilesFrom(GeoPosition target)
		{
			//TODO: Plug in Haversine Formula impl here
			throw new NotImplementedException();
		}

		public static GeoPosition FromAddress(string street, string city, string state, string zip)
		{
			double longDecDegrees = 0;
			double latDecDegrees = 0;
			//TODO: Calculate using Bing Locations API https://msdn.microsoft.com/en-us/library/ff701715.aspx or Google Maps Geocoding API https://developers.google.com/maps/documentation/geocoding/intro

			return new GeoPosition
			{
				LongDecDegrees = longDecDegrees,
				LatDecDegrees = latDecDegrees
			};
		}

		public static GeoPosition FromCity(string city, string state, string zip)
		{
			double longDecDegrees = 0;
			double latDecDegrees = 0;
			//TODO: Calculate using Bing Locations API https://msdn.microsoft.com/en-us/library/ff701715.aspx or Google Maps Geocoding API https://developers.google.com/maps/documentation/geocoding/intro

			return new GeoPosition
			{
				LongDecDegrees = longDecDegrees,
				LatDecDegrees = latDecDegrees
			};
		}
	}
}
