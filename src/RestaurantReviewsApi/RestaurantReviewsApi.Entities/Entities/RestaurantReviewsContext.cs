using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RestaurantReviewsApi.Entities
{
    public partial class RestaurantReviewsContext : DbContext
    {
        public RestaurantReviewsContext()
        {
        }

        public RestaurantReviewsContext(DbContextOptions<RestaurantReviewsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<Review> Review { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlserver;Database=RestaurantReviews;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.RestaurantId)
                    .HasName("PK_Restaurant_RestaurantId")
                    .IsClustered(false);

                entity.HasIndex(e => e.SystemId)
                    .HasName("CIX_Restaurant_SystemId")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.RestaurantId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SystemId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("PK_Review_ReviewId")
                    .IsClustered(false);

                entity.HasIndex(e => e.SystemId)
                    .HasName("CIX_Review_SystemId")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.ReviewId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SystemId).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
