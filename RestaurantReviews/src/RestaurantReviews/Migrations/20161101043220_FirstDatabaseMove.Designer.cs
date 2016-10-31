using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RestaurantReviews.DataAccess;

namespace RestaurantReviews.Migrations
{
    [DbContext(typeof(RestaurantReviewContext))]
    [Migration("20161101043220_FirstDatabaseMove")]
    partial class FirstDatabaseMove
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RestaurantReviews.Models.Address", b =>
                {
                    b.Property<long>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Address2")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Address3")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("City")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Country")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("State")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("ZipCode")
                        .HasAnnotation("MaxLength", 25);

                    b.HasKey("AddressId");

                    b.HasIndex("City");

                    b.HasIndex("ZipCode");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("RestaurantReviews.Models.Contact", b =>
                {
                    b.Property<long>("ContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AddressId");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Phone")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("ContactId");

                    b.HasIndex("AddressId");

                    b.HasIndex("Email");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("RestaurantReviews.Models.Restaurant", b =>
                {
                    b.Property<long>("RestaurantId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AddressId");

                    b.Property<long>("ContactId");

                    b.Property<string>("Cuisine")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<int>("DiningFormat");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("RestaurantId");

                    b.HasIndex("ContactId");

                    b.HasIndex("Name");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("RestaurantReviews.Models.Review", b =>
                {
                    b.Property<long>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<DateTime>("RatingDateTime");

                    b.Property<long>("RestaurantId");

                    b.Property<decimal>("Score");

                    b.Property<long>("UserId");

                    b.HasKey("ReviewId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("RestaurantReviews.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ContactInformationContactId");

                    b.Property<string>("FirstName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("LastName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("UserId");

                    b.HasIndex("ContactInformationContactId");

                    b.HasIndex("UserName");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RestaurantReviews.Models.Contact", b =>
                {
                    b.HasOne("RestaurantReviews.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RestaurantReviews.Models.Restaurant", b =>
                {
                    b.HasOne("RestaurantReviews.Models.Contact", "ContactInformation")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RestaurantReviews.Models.Review", b =>
                {
                    b.HasOne("RestaurantReviews.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RestaurantReviews.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RestaurantReviews.Models.User", b =>
                {
                    b.HasOne("RestaurantReviews.Models.Contact", "ContactInformation")
                        .WithMany()
                        .HasForeignKey("ContactInformationContactId");
                });
        }
    }
}
