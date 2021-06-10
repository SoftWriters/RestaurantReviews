using SQLite.Net.Platform.Win32;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Database.Sqlite.Tests
{
    internal static class TestDatabase
    {
        public static class Users
        {
            public static readonly FakeUser Jeremy = new FakeUser() { UniqueId = Guid.NewGuid(), DisplayName = "Jeremy the Programmer" };
            public static readonly FakeUser Angela = new FakeUser() { UniqueId = Guid.NewGuid(), DisplayName = "Angela the Wise" };
            public static readonly FakeUser Joshua = new FakeUser() { UniqueId = Guid.NewGuid(), DisplayName = "Joshua Legoman" };
            public static readonly FakeUser Caleb = new FakeUser() { UniqueId = Guid.NewGuid(), DisplayName = "Caleb Snuggles" };
            public static readonly FakeUser Isaac = new FakeUser() { UniqueId = Guid.NewGuid(), DisplayName = "Isaac Wontgotosleep" };

            public static IEnumerable<FakeUser> AllUsers = new[] { Jeremy, Angela, Joshua, Caleb, Isaac };
        }

        public static class Restaurants
        {
            public static readonly FakeRestaurant MadNoodles = new FakeRestaurant()
            {
                UniqueId = Guid.NewGuid(),
                Name = "Mad Noodles",
                Description = "Laid-back pick offering Asian specialities from Japanese noodles to sushi rolls plus lunch specials.",
                Address = new FakeAddress()
                {
                    StreetLine1 = "2017 E Carson St",
                    City = "Pittsburgh",
                    StateOrProvince = "PA",
                    PostalCode = "15203"
                }
            };

            public static readonly FakeRestaurant LittleTokyo = new FakeRestaurant()
            {
                UniqueId = Guid.NewGuid(),
                Name = "Little Tokyo",
                Description = "Vibrant eatery with a varied menu that includes sushi & hibachi plus wine, Japanese beers & sake.",
                Address = new FakeAddress()
                {
                    StreetLine1 = "636 Washington Rd",
                    City = "Pittsburgh",
                    StateOrProvince = "PA",
                    PostalCode = "15228"
                }
            };

            public static readonly FakeRestaurant DuckDonuts = new FakeRestaurant()
            {
                UniqueId = Guid.NewGuid(),
                Name = "Duck Donuts",
                Description = "Made to order donuts served warm and fresh.",
                Address = new FakeAddress()
                {
                    StreetLine1 = "1190 Duck Rd",
                    City = "Duck",
                    StateOrProvince = "NC",
                    PostalCode = "27949"
                }
            };

            public static readonly FakeRestaurant Dinos = new FakeRestaurant()
            {
                UniqueId = Guid.NewGuid(),
                Name = "Dino's Sports Lounge",
                Description = "Casual bar & restaurant serving wings, burgers & draft beer amid sports memorabilia & games on TV.",
                Address = new FakeAddress()
                {
                    StreetLine1 = "3883 US-30",
                    City = "Latrobe",
                    StateOrProvince = "PA",
                    PostalCode = "15650"
                }
            };

            public static readonly FakeRestaurant Sharkys = new FakeRestaurant()
            {
                UniqueId = Guid.NewGuid(),
                Name = "Sharky's Cafe",
                Description = "Informal tavern offering eclectic American grub & a full bar, plus multiple TVs for sports fans.",
                Address = new FakeAddress()
                {
                    StreetLine1 = "3960 Lincoln Hwy",
                    City = "Latrobe",
                    StateOrProvince = "PA",
                    PostalCode = "15650"
                }
            };

            public static IEnumerable<FakeRestaurant> AllRestaurants = new[] { MadNoodles, LittleTokyo, DuckDonuts, Dinos, Sharkys };
        }

        public static class Reviews
        {
            public static FakeRestaurantReview MadNoodles1 = new FakeRestaurantReview()
            {
                UniqueId = Guid.NewGuid(),
                Reviewer = Users.Angela,
                Restaurant = Restaurants.MadNoodles,
                ReviewText = "I’ve ordered from here multiple times in the last few weeks. The pineapple chicken fried rice, the dumplings, mango bubble tea, etc you name it, it’s DELICIOUS.",
                FiveStarRating = 5,
                Date = new DateTime(2021, 5, 8)
            };

            public static FakeRestaurantReview MadNoodles2 = new FakeRestaurantReview()
            {
                UniqueId = Guid.NewGuid(),
                Reviewer = Users.Caleb,
                Restaurant = Restaurants.MadNoodles,
                ReviewText = "The worst Asian food ever.  The entire order went to the garbage can!!! Low quality produce used to make all the dishes.  Ordered sushi tray, pad Thai with shrimp, fried calamari and Bubble tea. Everything been disposed.  The frozen food from Dollar general taste better than this place!!!!",
                FiveStarRating = 1,
                Date = new DateTime(2021, 4, 8)
            };

            public static FakeRestaurantReview MadNoodles3 = new FakeRestaurantReview()
            {
                UniqueId = Guid.NewGuid(),
                Reviewer = Users.Isaac,
                Restaurant = Restaurants.MadNoodles,
                ReviewText = "I’ve been meaning to get over here for a while and I regret not trying this place sooner! First, they have soup dumplings, which you can find everywhere. There a couple places in Pittsburgh, but they’re still rare. They were super good with nice warm broth and a savory meat ball inside. Just an amazing bite. It’s a high recommend for me. I also got the pad Thai which was massive. I got Spice level 3 and that was enough to make my nose run a little, which was perfect. The atmosphere was good with almost a cafe vibe music going on and everything relatively calm. My friends really liked their meals as well so try and make this a stop. The parking outside was free after 6 and I got a space 4 steps from the door. You won’t be disappointed!",
                FiveStarRating = 5,
                Date = new DateTime(2020, 6, 8)
            };

            public static FakeRestaurantReview LittleTokyo1 = new FakeRestaurantReview()
            {
                UniqueId = Guid.NewGuid(),
                Reviewer = Users.Jeremy,
                Restaurant = Restaurants.LittleTokyo,
                ReviewText = "Our food, as usual, was phenomenal. Our waitress, Irene, was even better. Kudos to her for a very natural ability to interact with our entire table in a very personal way. Thank you very much for a wonderful experience.",
                FiveStarRating = 5,
                Date = new DateTime(2021, 6, 4)
            };

            public static FakeRestaurantReview LittleTokyo2 = new FakeRestaurantReview()
            {
                UniqueId = Guid.NewGuid(),
                Reviewer = Users.Joshua,
                Restaurant = Restaurants.LittleTokyo,
                ReviewText = "A very nice Japanese restaurant for a quick lunch or a long dinner.  The ginger(?) sauce on the salad was rather salty, but that’s a small price for the quality of the meat dishes.  The waitresses are sweet, and the chefs cook the food right at the bar.  You also get a towel wipe with some bleach (nothing harmful) before your meal.  Good atmosphere.  Good service, but didn’t try the sushi.",
                FiveStarRating = 4,
                Date = new DateTime(2019, 6, 4)
            };

            public static FakeRestaurantReview Dinos1 = new FakeRestaurantReview()
            {
                UniqueId = Guid.NewGuid(),
                Reviewer = Users.Joshua,
                Restaurant = Restaurants.Dinos,
                ReviewText = "My parents and I go to this place in the other location several times a month. The chicken wings and pizza or favorites and sometimes the fish we get. We like the fries, and the waitresses are very attentive. We recommend this place to others as well as you.",
                FiveStarRating = 5,
                Date = new DateTime(2021, 6, 7)
            };

            public static FakeRestaurantReview Dinos2 = new FakeRestaurantReview()
            {
                UniqueId = Guid.NewGuid(),
                Reviewer = Users.Angela,
                Restaurant = Restaurants.Dinos,
                ReviewText = "My husband said the spicy breakfast burger was the best burger he ever had.   Lots of menu options has a bar and group seating area. But they should have another sweetener besides sweet and lo.",
                FiveStarRating = 4,
                Date = new DateTime(2021, 4, 7)
            };

            public static FakeRestaurantReview Sharkys1 = new FakeRestaurantReview()
            {
                UniqueId = Guid.NewGuid(),
                Reviewer = Users.Isaac,
                Restaurant = Restaurants.Sharkys,
                ReviewText = "I stopped into Sharky's when I was visiting town a few weeks ago, and I'm happy to say, that the wings are still excellent.  I've been coming here for 20 years -- it used to be every week, in high school; now it's whenever I come home to visit -- and the wings are the best in town.\n" +
                "Lindsey was great-- we held a pool table down for hours.\n" +
                "Get the Dragon - style!",
                FiveStarRating = 5,
                Date = new DateTime(2021, 6, 1)
            };

            public static IEnumerable<FakeRestaurantReview> AllReviews = new[]
            {
                MadNoodles1, MadNoodles2, MadNoodles3,
                LittleTokyo1, LittleTokyo2,
                Dinos1, Dinos2,
                Sharkys1
            };
        }
            
        public static SqliteRestaurantReviewDatabase CreateDatabase(string filePath)
        {
            var database = new SqliteRestaurantReviewDatabase(new SQLitePlatformWin32(), filePath);
        
            foreach (var user in Users.AllUsers)
            {
                database.AddUser(user);
            }

            foreach (var restaurant in Restaurants.AllRestaurants)
            {
                database.AddRestaurant(restaurant);
            }

            foreach (var review in Reviews.AllReviews)
            {
                database.AddReview(review);
            }

            return database;
        }
    }
}
