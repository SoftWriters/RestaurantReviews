using Microsoft.EntityFrameworkCore;
using RestaurantReviews.Data.Seed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace RestaurantReviews.Data
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        public RestaurantContext()
        {
            Init();
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            :base(options)
        {
            Init();
        }

        private void Init()
        {
            // This is a "poor-mans" record auditing
            // Another possibility is to use Db triggers
            ChangeTracker.Tracked += ChangeTracker_Tracked;
            ChangeTracker.StateChanged += ChangeTracker_StateChanged;
        }

        private void ChangeTracker_StateChanged(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
        {
            if (e.NewState == EntityState.Modified && e.Entry.Entity is IEntity entity)
            {
                entity.DateModified = DateTime.UtcNow;
            }
        }

        private void ChangeTracker_Tracked(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {
            if (!e.FromQuery && e.Entry.State == EntityState.Added && e.Entry.Entity is IEntity entity)
            {
                if (entity.Id == default)
                {
                    entity.Id = Guid.NewGuid();
                }
                entity.DateCreated = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Insert seed data
            modelBuilder.Entity<Restaurant>()
                .HasData(SeedRestaurants.All);
            modelBuilder.Entity<User>()
                .HasData(SeedUsers.All);
            modelBuilder.Entity<Review>()
                .HasData(SeedReviews.All);
        }
    }
}
