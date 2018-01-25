using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supr.Model.Entity {
	public class Restaurant {

		[Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
		public int Id { get; set; }

		[Required, MaxLength( 100 )]
		public string Name { get; set; }

		[MaxLength( 100 )]
		public string Street { get; set; }

		[Required, MaxLength( 50 )]
		public string City { get; set; }

		[Required, MaxLength( 2 )]
		public string State { get; set; }

		[MaxLength( 6 )]
		public string ZipCode { get; set; }

		[MaxLength( 50 )]
		public string Category { get; set; }

		[DatabaseGenerated( DatabaseGeneratedOption.Computed )]
		public DateTime Created { get; set; }
	}
}