using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Restaurant_Reviews
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Database.SetInitializer(new InitializeDataBaseWithSeedData());

            Repository repo = new Repository();

            List<string> Names = repo.GetResaurantsByCity("Grove City");

            Console.WriteLine("Restaurants in Grove City");
            foreach (string city in Names)
            {
                Console.WriteLine(city);
            }
            Console.WriteLine();

            Restaurant restaurant = new Restaurant { Name = "Hoss's", City = "Pine Township" };
            repo.SaveRestaurant(restaurant);

            Restaurant rest;
            User user;
            using (var db = new RRContext())
            {
                var query = from r in db.Resturants
                            where r.Name == "Burger King"
                            select r;

                rest = query.SingleOrDefault();

                var query2 = from u in db.Users
                             where u.Name == "Tim"
                             select u;

                user = query2.SingleOrDefault();
            }
            repo.SaveReview(rest, "Good Whopper", user);

            List<RestaurantReviewView> reviews = repo.GetReviewsByUser(1);

            Console.WriteLine("Reviews by UserID = 1");
            foreach (RestaurantReviewView r in reviews)
            {
                Console.WriteLine(r.ReviewText);
            }

            Console.WriteLine();

            repo.DeleteReview(1);

            Console.ReadLine();
            //}
        }
    }
}