using Abp.EntityFramework;
using System.Data.Entity;

namespace RestaurantReviews.EntityFramework
{
    public class RestaurantReviewsDbContext : AbpDbContext
    {
        public virtual IDbSet<Review> Reviews { get; set; }
        public virtual IDbSet<Restaurant> Restaurants { get; set; }
        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<City> Cities { get; set; }
        public virtual IDbSet<State> States { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public RestaurantReviewsDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in RestaurantReviewsDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of RestaurantReviewsDbContext since ABP automatically handles it.
         */
        public RestaurantReviewsDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }
}
