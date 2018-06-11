using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RestaurantReviews.API.Data.SqlServer.DataModel
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

        public virtual DbSet<TblRestaurant> TblRestaurant { get; set; }
        public virtual DbSet<TblReview> TblReview { get; set; }
        public virtual DbSet<TblReviewer> TblReviewer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV12;Initial Catalog=RestaurantReviews;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblRestaurant>(entity =>
            {
                entity.ToTable("tblRestaurant");

                entity.HasIndex(e => new { e.Name, e.City })
                    .HasName("UNQ_tblRestaurant_Name_City")
                    .IsUnique();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblReview>(entity =>
            {
                entity.ToTable("tblReview");

                entity.Property(e => e.ReviewDateTime)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReviewText)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.TblReview)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblReview_tblRestaurant");

                entity.HasOne(d => d.Reviewer)
                    .WithMany(p => p.TblReview)
                    .HasForeignKey(d => d.ReviewerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblReview_tblReviewer");
            });

            modelBuilder.Entity<TblReviewer>(entity =>
            {
                entity.ToTable("tblReviewer");

                entity.HasIndex(e => e.Name)
                    .HasName("UNQ_tblReviewer_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
