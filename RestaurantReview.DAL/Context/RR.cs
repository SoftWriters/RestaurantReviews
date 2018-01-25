using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using RestaurantReview.DAL.Interface;
using RestaurantReview.DAL.Entity;

namespace RestaurantReview.DAL.Context
{
    public partial class RR : DbContext, IRRContext
    {
        public RR()
            : base("name=RR")
        {
        }

        public virtual IDbSet<City> Cities { get; set; }
        public virtual IDbSet<Restaurant> Restaurants { get; set; }
        public virtual IDbSet<Review> Reviews { get; set; }
        public virtual IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .Property(e => e.CityName)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Restaurants);
                //.WithRequired(e => e.City)
                //.WillCascadeOnDelete(false);

            modelBuilder.Entity<Restaurant>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Restaurant>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Restaurant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }

        public override int SaveChanges()
        {
            //TODO: Auditing requirements

            return base.SaveChanges();
        }
    }
}