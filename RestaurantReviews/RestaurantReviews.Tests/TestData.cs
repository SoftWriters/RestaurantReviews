using RestaurantReviews.Controller;
using RestaurantReviews.Core.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    /// <summary>
    /// Semi-fabricated sample data for unit tests. Some restaurants and reviews are based on real Google Reviews.  
    /// </summary>
    internal static class TestData
    {
        public static class Users
        {
            public static readonly User Jeremy = new User() { UniqueId = Guid.NewGuid(), DisplayName = "Jeremy the Programmer" };
            public static readonly User Angela = new User() { UniqueId = Guid.NewGuid(), DisplayName = "Angela the Wise" };
            public static readonly User Joshua = new User() { UniqueId = Guid.NewGuid(), DisplayName = "Joshua Legoman" };
            public static readonly User Caleb = new User() { UniqueId = Guid.NewGuid(), DisplayName = "Caleb Snuggles" };
            public static readonly User Isaac = new User() { UniqueId = Guid.NewGuid(), DisplayName = "Isaac Wontgotosleep" };

            public static IEnumerable<User> AllUsers = new[] { Jeremy, Angela, Joshua, Caleb, Isaac };
        }

        public static class Restaurants
        {
            //TODO: Maybe create the guids once so we can re-use the populated db
            public static readonly Restaurant MadNoodles = new Restaurant()
            {
                Name = "Mad Noodles",
                Description = "Laid-back pick offering Asian specialities from Japanese noodles to sushi rolls plus lunch specials.",
                Address = new Address()
                {
                    StreetLine1 = "2017 E Carson St",
                    City = "Pittsburgh",
                    StateOrProvince = "PA",
                    PostalCode = "15203"
                }
            };

            public static readonly Restaurant LittleTokyo = new Restaurant()
            {
                Name = "Little Tokyo",
                Description = "Vibrant eatery with a varied menu that includes sushi & hibachi plus wine, Japanese beers & sake.",
                Address = new Address()
                {
                    StreetLine1 = "636 Washington Rd",
                    City = "Pittsburgh",
                    StateOrProvince = "PA",
                    PostalCode = "15228"
                }
            };

            public static readonly Restaurant DuckDonuts = new Restaurant()
            {
                Name = "Duck Donuts",
                Description = "Made to order donuts served warm and fresh.",
                Address = new Address()
                {
                    StreetLine1 = "1190 Duck Rd",
                    City = "Duck",
                    StateOrProvince = "NC",
                    PostalCode = "27949"
                }
            };

            public static readonly Restaurant Dinos = new Restaurant()
            {
                Name = "Dino's Sports Lounge",
                Description = "Casual bar & restaurant serving wings, burgers & draft beer amid sports memorabilia & games on TV.",
                Address = new Address()
                {
                    StreetLine1 = "3883 US-30",
                    City = "Latrobe",
                    StateOrProvince = "PA",
                    PostalCode = "15650"
                }
            };

            public static readonly Restaurant Sharkys = new Restaurant()
            {
                Name = "Sharky's Cafe",
                Description = "Informal tavern offering eclectic American grub & a full bar, plus multiple TVs for sports fans.",
                Address = new Address()
                {
                    StreetLine1 = "3960 Lincoln Hwy",
                    City = "Latrobe",
                    StateOrProvince = "PA",
                    PostalCode = "15650"
                }
            };

            public static IEnumerable<Restaurant> AllRestaurants = new[] { MadNoodles, LittleTokyo, DuckDonuts, Dinos, Sharkys };
        }

        public static class Reviews
        {
            public static RestaurantReview MadNoodles1 = new RestaurantReview()
            {
                Reviewer = Users.Angela,
                RestaurantUniqueId = Restaurants.MadNoodles.UniqueId,
                ReviewText = "I’ve ordered from here multiple times in the last few weeks. The pineapple chicken fried rice, the dumplings, mango bubble tea, etc you name it, it’s DELICIOUS.",
                FiveStarRating = 5,
                Timestamp = new DateTime(2021, 5, 8, 0, 0, 0, DateTimeKind.Utc)
            };

            public static RestaurantReview MadNoodles2 = new RestaurantReview()
            {
                Reviewer = Users.Caleb,
                RestaurantUniqueId = Restaurants.MadNoodles.UniqueId,
                ReviewText = "The worst Asian food ever.  The entire order went to the garbage can!!! Low quality produce used to make all the dishes.  Ordered sushi tray, pad Thai with shrimp, fried calamari and Bubble tea. Everything been disposed.  The frozen food from Dollar general taste better than this place!!!!",
                FiveStarRating = 1,
                Timestamp = new DateTime(2021, 4, 8, 0, 0, 0, DateTimeKind.Utc)
            };

            public static RestaurantReview MadNoodles3 = new RestaurantReview()
            {
                Reviewer = Users.Isaac,
                RestaurantUniqueId = Restaurants.MadNoodles.UniqueId,
                ReviewText = "I’ve been meaning to get over here for a while and I regret not trying this place sooner! First, they have soup dumplings, which you can find everywhere. There a couple places in Pittsburgh, but they’re still rare. They were super good with nice warm broth and a savory meat ball inside. Just an amazing bite. It’s a high recommend for me. I also got the pad Thai which was massive. I got Spice level 3 and that was enough to make my nose run a little, which was perfect. The atmosphere was good with almost a cafe vibe music going on and everything relatively calm. My friends really liked their meals as well so try and make this a stop. The parking outside was free after 6 and I got a space 4 steps from the door. You won’t be disappointed!",
                FiveStarRating = 5,
                Timestamp = new DateTime(2020, 6, 8, 0, 0, 0, DateTimeKind.Utc)
            };

            public static RestaurantReview LittleTokyo1 = new RestaurantReview()
            {
                Reviewer = Users.Jeremy,
                RestaurantUniqueId = Restaurants.LittleTokyo.UniqueId,
                ReviewText = "Our food, as usual, was phenomenal. Our waitress, Irene, was even better. Kudos to her for a very natural ability to interact with our entire table in a very personal way. Thank you very much for a wonderful experience.",
                FiveStarRating = 5,
                Timestamp = new DateTime(2021, 6, 4, 0, 0, 0, DateTimeKind.Utc)
            };

            public static RestaurantReview LittleTokyo2 = new RestaurantReview()
            {
                Reviewer = Users.Joshua,
                RestaurantUniqueId = Restaurants.LittleTokyo.UniqueId,
                ReviewText = "A very nice Japanese restaurant for a quick lunch or a long dinner.  The ginger(?) sauce on the salad was rather salty, but that’s a small price for the quality of the meat dishes.  The waitresses are sweet, and the chefs cook the food right at the bar.  You also get a towel wipe with some bleach (nothing harmful) before your meal.  Good atmosphere.  Good service, but didn’t try the sushi.",
                FiveStarRating = 4,
                Timestamp = new DateTime(2019, 6, 4, 0, 0, 0, DateTimeKind.Utc)
            };

            public static RestaurantReview Dinos1 = new RestaurantReview()
            {
                Reviewer = Users.Joshua,
                RestaurantUniqueId = Restaurants.Dinos.UniqueId,
                ReviewText = "My parents and I go to this place in the other location several times a month. The chicken wings and pizza or favorites and sometimes the fish we get. We like the fries, and the waitresses are very attentive. We recommend this place to others as well as you.",
                FiveStarRating = 5,
                Timestamp = new DateTime(2021, 6, 7, 0, 0, 0, DateTimeKind.Utc)
            };

            public static RestaurantReview Dinos2 = new RestaurantReview()
            {
                Reviewer = Users.Angela,
                RestaurantUniqueId = Restaurants.Dinos.UniqueId,
                ReviewText = "My husband said the spicy breakfast burger was the best burger he ever had.   Lots of menu options has a bar and group seating area. But they should have another sweetener besides sweet and lo.",
                FiveStarRating = 4,
                Timestamp = new DateTime(2021, 4, 7, 0, 0, 0, DateTimeKind.Utc)
            };

            public static RestaurantReview Sharkys1 = new RestaurantReview()
            {
                Reviewer = Users.Isaac,
                RestaurantUniqueId = Restaurants.Sharkys.UniqueId,
                ReviewText = "I stopped into Sharky's when I was visiting town a few weeks ago, and I'm happy to say, that the wings are still excellent.  I've been coming here for 20 years -- it used to be every week, in high school; now it's whenever I come home to visit -- and the wings are the best in town.\n" +
                "Lindsey was great-- we held a pool table down for hours.\n" +
                "Get the Dragon - style!",
                FiveStarRating = 5,
                Timestamp = new DateTime(2021, 6, 1, 0, 0, 0, DateTimeKind.Utc)
            };

            public static IEnumerable<RestaurantReview> AllReviews = new[]
            {
                MadNoodles1, MadNoodles2, MadNoodles3,
                LittleTokyo1, LittleTokyo2,
                Dinos1, Dinos2,
                Sharkys1
            };
        }
            
        public static IRestaurantReviewController InitializeController(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath); //Overwrite the old one

            var controller = new RestaurantReviewsController(filePath);
        
            foreach (var restaurant in Restaurants.AllRestaurants)
            {
                controller.AddRestaurant(restaurant);
            }

            foreach (var review in Reviews.AllReviews)
            {
                controller.AddReview(review);
            }

            return controller;
        }
    }
}
