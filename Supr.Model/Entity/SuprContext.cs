using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Supr.Model.Entity {
	public class SuprContext : DbContext {
		public SuprContext()
			 : base( "name=SuprConnection" ) {
		}

		protected override void OnModelCreating( DbModelBuilder modelBuilder ) {
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		public virtual DbSet<Restaurant> Restaurant { get; set; }
		public virtual DbSet<Review> Review { get; set; }
	}
}