using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWriters.CandidateSamples.ChrisGrella.RestaurantReviews.ObjectModel
{
	public class Address : PersistedObject<Address>
	{
		public string StreetLine1 { get; set; }
		public string StreetLine2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public GeoPosition GeoPosition { get; protected set; }

		public bool IsWithinNMilesOf(double distanceMiles, Address target)
		{
			if (target == null)
				throw new ArgumentNullException("target");

			return GeoPosition.GetDistanceInMilesFrom(target.GeoPosition) <= distanceMiles;
		}

		public bool IsWithinNMilesOf(double distanceMiles, string city, string state, string zip)
		{
			return GeoPosition.FromCity(city, state, zip).GetDistanceInMilesFrom(GeoPosition) <= distanceMiles;
		}

		public Address() { }

		public Address(string streetLine1, string streetLine2, string city, string state, string zip)
		{
			StreetLine1 = streetLine1;
			StreetLine2 = streetLine2;
			City = city;
			State = state;
			Zip = zip;
			GeoPosition = GeoPosition.FromAddress(StreetLine1, City, State, Zip);
		}
    }
}
