using System;
using System.Collections.Generic;
using System.Linq;
using SoftWriters.RestaurantReviews.DataLibrary;

namespace ClientConsole
{
    // Since unit tests test the API pretty well, we only use this console client to make sure the plumbing
    // is in place and functions correctly. We don't necessarily need complete functionality here but you can
    // go ahead and add functionality as needed.
    // NOTE: This is good for say, a working demo before UI gets created.
    class Program
    {
        private static string Instructions =
            "\nType the appropriate number that corresponds to the api call you would like to test, then press ENTER:" +
            "\n\t0\tExit" +
            "\n\t1\tGet Restaurants By City" +
            "\n\t2\tGet Restaurants By Zip" +
            "\n\t3\tGet All Restaurants" +
            "\n\t4\tAdd Restaurant" + 
            "\n\t5\tAdd Review" + 
            "\n\t6\tGet Reviews by user" + 
            "\n\t7\tGet Reviews by restaurant";

        // We don't have any concept of logging in, so this will represent the current user
        private static Guid UserId = Guid.Parse("611C77C4-DA99-4674-8252-87C9923A47D3");

        static void Main(string[] args)
        {
            var reviewClient = new ReviewClient();
            Console.WriteLine("Connected!\n");
            Console.WriteLine(Instructions);

            while (true)
            {
                var input = Console.ReadLine().Trim().ToLower();
                
                switch (input)
                {
                    case "0":
                    case "q":
                        reviewClient.Close();
                        Environment.Exit(0);
                        break;

                    case "1":
                        GetRestaurantsByCity(reviewClient);
                        break;

                    case "2":
                        GetRestaurantsByZip(reviewClient);
                        break;

                    case "3":
                        GetAllRestaurants(reviewClient);
                        break;

                    case "4":
                       AddRestaurant(reviewClient);
                        break;

                    case "5":
                        AddReview(reviewClient);
                        break;

                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }

                Console.WriteLine(Instructions);
                Console.Write(":");
            }
        }

        static void GetRestaurantsByCity(ReviewClient reviewClient)
        {
            Console.Write("Enter city: ");
            var city = Console.ReadLine();
            Console.WriteLine();
            var restaurants = reviewClient.GetRestaurants("", city, "", "", "");
            PrintRestaurants(restaurants);
        }

        static void GetRestaurantsByZip(ReviewClient reviewClient)
        {
            Console.Write("Enter zip code: ");
            var postalCode = Console.ReadLine();
            Console.WriteLine();
            var restaurants = reviewClient.GetRestaurants("", "", "", postalCode, "");
            PrintRestaurants(restaurants);
        }

        static void GetAllRestaurants(ReviewClient reviewClient)
        {
            Console.WriteLine("Get all restaurants");
            var restaurants = reviewClient.GetRestaurants("", "", "", "", "");
            PrintRestaurants(restaurants);
        }

        static void AddRestaurant(ReviewClient reviewClient)
        {
            Console.Write("Enter restaurant name: ");
            var restaurantName = Console.ReadLine();
            Console.Write("\nEnter street address: ");
            var restaurantStreet = Console.ReadLine();
            Console.Write("\nEnter city: ");
            var restaurantCity = Console.ReadLine();
            Console.Write("\nEnter state: ");
            var restaurantState = Console.ReadLine();
            Console.Write("\nEnter postal code: ");
            var restaurantZip = Console.ReadLine();
            Console.Write("\nEnter country: ");
            var restaurantCountry = Console.ReadLine();

            bool result = reviewClient.AddRestaurant(restaurantName, restaurantStreet, restaurantCity, restaurantState,
                restaurantZip, restaurantCountry);

            Console.WriteLine("\n{0}", result ? "Add successful" : "Add failed");
        }

        static void AddReview(ReviewClient reviewClient)
        {
            Console.WriteLine("Enter the number corresponding to ");

            var restaurants = reviewClient.GetRestaurants("", "", "", "", "").ToList();
            
            TrySelectRestaurant(restaurants, out Restaurant restaurant);
            Console.Write("\nEnter overall rating (1-5): ");
            var overallRating = int.TryParse(Console.ReadLine(), out int oRating) ? oRating : 0;

            Console.Write("\nEnter food rating (1-5): ");
            var foodRating = int.TryParse(Console.ReadLine(), out int fRating) ? fRating : 0;

            Console.Write("\nEnter service rating (1-5): ");
            var serviceRating = int.TryParse(Console.ReadLine(), out int sRating) ? sRating : 0;

            Console.Write("\nEnter cost rating (1-5): ");
            var costRating = int.TryParse(Console.ReadLine(), out int cRating) ? cRating : 0;

            Console.Write("Enter comments: ");
            var comments = Console.ReadLine();

            bool result = reviewClient.AddReview(UserId, restaurant.Id, overallRating, foodRating, serviceRating, costRating, comments);

            Console.WriteLine("\n{0}", result ? "Add successful" : "Add failed");
        }

        static bool TrySelectRestaurant(List<Restaurant> restaurants, out Restaurant selection)
        {
            selection = restaurants[0];

            Console.WriteLine("Select restaurant (type the number and press Enter)");
            for (int i = 0; i < restaurants.Count; i++)
            {
                Console.WriteLine("[{0}]\t{1}", i, restaurants[i].Name);
            }

            var input = Console.ReadLine();
            if (!int.TryParse(input, out int index))
                return false;

            if (index >= restaurants.Count)
                return false;

            selection = restaurants[index];
            return true;
        }

        static void PrintRestaurants(IEnumerable<Restaurant> restaurants)
        {
            foreach (var item in restaurants)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
