using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWriters.CandidateSamples.ChrisGrella.RestaurantReviews.ObjectModel
{
	public class Restaurant : PersistedObject<Restaurant>
	{
		public string Name { get; set; }
		public Address Address { get; set; }
		public List<Cuisine> Cuisines { get; set; }

		public Dictionary<DayOfWeek, DoWSpecificHoursOfOperation> HoursOfOperation { get; set; }
		public string HoursOfOperationDisplay(TimeZone localTimeZone)
		{
			//CONSIDER: This could use some finesse, like when M-F hours are all the same, do "M-F 11:00 am - 11:00 pm", but you get the point.
			return string.Join(", ", HoursOfOperation.Values.Select(h => h.ToDisplayString(localTimeZone)));
		}

		public List<Review> Reviews { get; set; }
		public int AverageRating
		{
			get
			{
				//TODO: Handle "no ratings" scenario
				return Convert.ToInt32(Reviews.Average(r => r.Rating));
			}
		}
		public PriceRange PriceRange { get; set; }
		public DateTime Opened { get; set; }
		public bool ServesBreakfast { get; set; }
		public bool ServesLunch { get; set; }
		public bool ServesDinner { get; set; }
		public bool ServesLateNight { get; set; }
		public bool TakesPhoneReservations { get; set; }
		public bool TakesOnlineReservations { get; set; }
		public double DeliveryRangeMiles { get; set; }
		public bool Delivers
		{
			get
			{
				return DeliveryRangeMiles > 0;
			}
		}
		public bool TakesOnlineDeliveryOrders { get; set; }

		public static Restaurant Add(string name, Address address, List<Cuisine> cuisines, Dictionary<DayOfWeek, DoWSpecificHoursOfOperation> hoursOfOperation,
			PriceRange priceRange, DateTime opened, bool servesBreakfast, bool servesLunch, bool servesDinner, bool servesLateNight,
			bool takesPhoneReservations, bool takesOnlineReservations, double deliveryRangeMiles, bool takesOnlineDeliveryOrders)
		{
			Restaurant newRestaurant = new Restaurant
			{
				Name = name,
				Address = address,
				Cuisines = cuisines,
				HoursOfOperation = hoursOfOperation,
				PriceRange = priceRange,
				Opened = opened,
				ServesBreakfast = servesBreakfast,
				ServesLunch = servesLunch,
				ServesDinner = servesDinner,
				ServesLateNight = servesLateNight,
				TakesPhoneReservations = takesPhoneReservations,
				TakesOnlineReservations = takesOnlineReservations,
				DeliveryRangeMiles = deliveryRangeMiles,
				TakesOnlineDeliveryOrders = takesOnlineDeliveryOrders
			};

			//TODO: Persist via separate layer or ORM attributes (EntityFramework, NHibernate, etc)

			return newRestaurant;
		}

		public static List<Restaurant> FindAllByCity(string city, string state, string zip, double distanceMiles) //TODO add as optional paramters search criteria per all properties
		{
			//TODO: Need persistence layer for lookup
			throw new NotImplementedException();
		}

		public static List<Restaurant> FindAllByMyLocation(double longDecDegrees, double latDecDegrees) //TODO add as optional paramters search criteria per all properties
		{
			//TODO: Need persistence layer for lookup
			throw new NotImplementedException();
		}
	}
}
