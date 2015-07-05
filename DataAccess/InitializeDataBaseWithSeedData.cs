using Model;
using System.Collections.Generic;
using System.Data.Entity;

namespace DataAccess
{
    public class InitializeDataBaseWithSeedData : DropCreateDatabaseAlways<RRContext> // CreateDatabaseIfNotExists<RRContext>
    {
        protected override void Seed(RRContext context)
        {
            context.Resturants.Add(new Restaurant
            {
                Name = "Grove City Diner",
                City = "Grove City"
            });
            context.Resturants.Add(new Restaurant
            {
                Name = "Burger King",
                City = "Pittsburgh"
            });

            context.Resturants.Add(new Restaurant
            {
                Name = "Perkins",
                City = "Grove City"
            });
            context.Resturants.Add(new Restaurant
            {
                Name = "Kings",
                City = "Pine Township",
                Owner = "Owner1"
            });

            context.Resturants.Add(new Restaurant
            {
                Name = "Boston Market",
                City = "Monroeville",
                Owner = "Owner1"
            });

            context.Resturants.Add(new Restaurant
            {
                Name = "Jordans",
                City = "Grove City",
                Reviews = new List<Review> {
                    new Review { ReviewText = "Good Greek Pizza.",
                        User = new User { Name = "Cindy"}
                    },
                    new Review { ReviewText = "Good Pancit",
                        User = new User { Name = "Tim"}
                    }
                }
            }

                );

            context.Resturants.Add(new Restaurant
            {
                Name = "Jordans",
                Reviews = new List<Review> { new Review { ReviewText = "Good Greek Pizza.", User = new User { Name = "Joe" } } }
            }

                );
        }
    }
}