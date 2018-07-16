using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Entities
{
	public class Restaurant
	{

		public int RestaurantId { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public Nullable<int> Zipcode { get; set; }
		

	}
}
