using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Entities
{
	public class RestaurantReview
	{
		public int RevId { get; set; }
		public string RestaurantName { get; set; }
		public string Comments { get; set; }
		public int Ratings { get; set; }
	}
}
