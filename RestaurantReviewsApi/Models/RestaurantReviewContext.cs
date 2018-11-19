using System;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReviews.Api.Models
{
    public class RestaurantReviewContext : DbContext
    {
         ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the restaurant reviews. </summary>
        ///
        /// <value> The restaurant reviews. </value>
        /// 
        ///-------------------------------------------------------------------------------------------------
        public DbSet<RestaurantReview> RestaurantReviews { get; set; }

       ///-------------------------------------------------------------------------------------------------
        /// <summary> EntityFrasmework DBContext Constructor.</summary>
        ///
        /// <param name="options">  Options for controlling the operation. </param>
        ///-------------------------------------------------------------------------------------------------
        public RestaurantReviewContext(DbContextOptions<RestaurantReviewContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<RestaurantReview>()
            //    .Property(s => s.Id)
            //    .HasColumnName("Id")
            //    .HasDefaultValue(new Guid())
            //    .IsRequired();

            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
        }

    }
}