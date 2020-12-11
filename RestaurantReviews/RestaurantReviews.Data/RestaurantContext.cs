using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<User>()
                .HasData(
                new User { Id = Guid.Parse("43757037-6429-4aa0-8c96-62867c419967"), First = "Homer", Last = "Simpson" },
                new User { Id = Guid.Parse("ac6b3f45-2cdf-4a2d-8df1-764f25ca8614"), First = "Marge", Last = "Simpson" },
                new User { Id = Guid.Parse("41355f76-973d-47cd-9c48-90ae826d9a0c"), First = "Bart", Last = "Simpson" },
                new User { Id = Guid.Parse("779ab260-7af2-421a-b0cf-4df17164a406"), First = "Lisa", Last = "Simpson" },
                new User { Id = Guid.Parse("292801c5-a3ca-4f35-9224-159ec9667e11"), First = "Maggie", Last = "Simpson" });
        }
    }
}
