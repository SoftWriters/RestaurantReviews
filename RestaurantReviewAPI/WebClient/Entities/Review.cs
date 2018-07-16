using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Entities
{
	public class Reviews
	{

		public int Id { get; set; }
		public int UserId { get; set; }
		public int RestaurantId { get; set; }
		public string Name { get; set; }
		public string Comment { get; set; }
		public int Stars { get; set; }


	}
}
