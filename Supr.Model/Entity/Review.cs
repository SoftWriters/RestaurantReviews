using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supr.Model.Entity {
	public class Review {
		[Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
		public int Id { get; set; }
		public int RestaurantId { get; set; }

		public string Comments { get; set; }

		public int Rating { get; set; }
		public string UserId { get; set; }

		[DatabaseGenerated( DatabaseGeneratedOption.Computed )]
		public DateTime Created { get; set; }
	}
}
