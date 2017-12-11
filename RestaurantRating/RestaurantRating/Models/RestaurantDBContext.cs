using RestaurantRating.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRating.Models
{
    public class RestaurantDBContext : DbContext
    {

        public RestaurantDBContext()
            : base("name = DefaultConnection")
        { }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>()
            .HasMany<Restaurant>(r => r.restaurant)
            .WithMany(u => u.user)
            .Map(m =>
            {
                m.ToTable("Users");
                m.MapLeftKey("RestaurantName");
                m.MapRightKey("Id");
            });


            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RestaurantDBContext, Configuration>());
            base.OnModelCreating(modelBuilder);

        }



    }
}