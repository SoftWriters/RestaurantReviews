using Models;
using NUnit.Framework;
using Repositories;
using System.Collections.Generic;

namespace UnitTesting.Repositories.Tests
{
    // Notes:
    // 
    // I am not using mock objects for the Models, because they are all standard C# objects.  If 
    // they don't work, there are larger problems that will prevent these tests from running in 
    // the first place.  Technically that makes these more Integration Tests, but I think I can
    // get away with calling them Unit Tests.
    //
    // Possible changes:
    //
    // Refactor setup to create repositories which are used instead of objects of type List<> inside
    //        each individual [Test] method.
    //
    // Pull each repository into its own test file instead of testing all repositories in one file.
    //        This sample has a small number of repositories, and each repository has a small number of
    //        methods to test, so it's probably OK here, but I'm on the fence aleady, so I probably 
    //        have done it that way.
    //
    [TestFixture]
    public class Repositories
    {
        List<IChainModel> _chains;
        List<ICityModel> _cities;
        List<IRestaurantModel> _restaurants;
        List<IReviewModel> _reviews;
        List<IUserModel> _users;

        [OneTimeSetUp]
        public void Initialize()
        {
            _chains = new List<IChainModel>();
            _cities = new List<ICityModel>();
            _restaurants = new List<IRestaurantModel>();
            _reviews = new List<IReviewModel>();
            _users = new List<IUserModel>();

            var mon = new CityModel("Monroeville, PA");
            var oak = new CityModel("Oakland, PA");
            var mcc = new CityModel("McCandless, PA");
            var asp = new CityModel("Aspinwall, PA");
            var pit = new CityModel("Pittsburgh, PA");
            var rob = new CityModel("Robinson Township, PA");

            var og = new ChainModel("Olive Garden");
            var mm = new ChainModel("Mad Mex");

            var mmoak = new RestaurantModel("Mad Mex", oak, mm, "Atwood Street");
            var mmmcc = new RestaurantModel("Mad Mex", mcc, mm, "McKnight Road");
            var mmmon = new RestaurantModel("Mad Mex", mon, mm, "Miracle Mile");
            var ogmon = new RestaurantModel("Olive Garden", mon, og, "Monroeville Mall");
            var ogmcc = new RestaurantModel("Olive Garden", mcc, og, "Ross Park Mall");
            var ogrob = new RestaurantModel("Olive Garden", rob, og, "Fayette Center");
            var corner = new RestaurantModel("Cornerstone", asp, null, "Freeport Road");
            var donp = new RestaurantModel("Don Parmesan's", mcc, null, "McKnight Road");

            var wbf = new UserModel("Bill Fisher", true);
            var joe = new UserModel("Joe Smith", false);

            var rev1 = new ReviewModel(wbf, mmoak, 4, 5, 4, 4, 5, "Eat here all the time");
            var rev2 = new ReviewModel(wbf, donp, 1, 1, 1, 1, 1, "Went out of business!");
            var rev3 = new ReviewModel(joe, mmoak, 4, 4, 4, 4, 4, "It's not truly Mexican food.");

            _chains.Add(og);
            _chains.Add(mm);

            _cities.Add(mon);
            _cities.Add(oak);
            _cities.Add(mcc);
            _cities.Add(asp);
            _cities.Add(pit);
            _cities.Add(rob);

            _restaurants.Add(mmoak);
            _restaurants.Add(mmmcc);
            _restaurants.Add(mmmon);
            _restaurants.Add(ogmon);
            _restaurants.Add(ogmcc);
            _restaurants.Add(ogrob);
            _restaurants.Add(corner);
            _restaurants.Add(donp);

            _reviews.Add(rev1);
            _reviews.Add(rev2);
            _reviews.Add(rev3);

            _users.Add(wbf);
            _users.Add(joe);
        }

        [Test]
        public void TestAddChain()
        {
            ChainRepository chainRepo = new ChainRepository();

            Assert.That(chainRepo.HasData, Is.False);
            Assert.That(chainRepo.GetChainById(1), Is.Null);

            foreach (var chain in _chains)
            {
                chainRepo.AddChain(chain);
            }

            Assert.That(chainRepo.HasData, Is.True);
            Assert.That(chainRepo.GetChainById(1), Is.Not.Null);
        }

        [Test]
        public void TestAddCity()
        {
            CityRepository cityRepo = new CityRepository();

            Assert.That(cityRepo.HasData, Is.False);
            Assert.That(cityRepo.GetCityById(1), Is.Null);

            foreach(var city in _cities)
            {
                cityRepo.AddCity(city);
            }

            Assert.That(cityRepo.HasData, Is.True);
            Assert.That(cityRepo.GetCityById(1), Is.Not.Null);
        }
        
        [Test]
        public void TestAddRestaurant()
        {
            RestaurantRepository restRepo = new RestaurantRepository();

            Assert.That(restRepo.HasData, Is.False);
            Assert.That(restRepo.GetRestaurantById(1), Is.Null);

            foreach (var rest in _restaurants)
            {
                restRepo.AddRestaurant(rest);
            }

            Assert.That(restRepo.HasData, Is.True);
            Assert.That(restRepo.GetRestaurantById(1), Is.Not.Null);
        }

        [Test] 
        public void TestGetRestaurants()
        {
            RestaurantRepository restRepo = new RestaurantRepository();

            Assert.That(restRepo.HasData, Is.False);
            Assert.That(restRepo.GetRestaurants(), Has.Count.EqualTo(0));

            foreach (var rest in _restaurants)
            {
                restRepo.AddRestaurant(rest);
            }

            Assert.That(restRepo.HasData, Is.True);
            Assert.That(restRepo.GetRestaurants(), Has.Count.EqualTo(8));
        }

        [Test]
        public void TestGetRestaurantsByCity()
        {
            RestaurantRepository restRepo = new RestaurantRepository();
            var mon = _cities.Find(c => c.Name == "Monroeville, PA");

            Assert.That(restRepo.GetRestaurantsByCity(mon), Has.Count.EqualTo(0));

            foreach (var rest in _restaurants)
            {
                restRepo.AddRestaurant(rest);
            }

            Assert.That(restRepo.GetRestaurantsByCity(mon), Has.Count.EqualTo(2));
        }

        [Test]
        public void TestAddReview()
        {
            ReviewRepository revRepo = new ReviewRepository();

            Assert.That(revRepo.HasData, Is.False);
            Assert.That(revRepo.GetReviewById(1), Is.Null);

            foreach (var rev in _reviews)
            {
                revRepo.AddReview(rev);
            }

            Assert.That(revRepo.HasData, Is.True);
            Assert.That(revRepo.GetReviewById(1), Is.Not.Null);
        }
        
        [Test]
        public void TestDeleteReview()
        {
            ReviewRepository revRepo = new ReviewRepository();

            foreach (var rev in _reviews)
            {
                revRepo.AddReview(rev);
            }

            Assert.That(revRepo.GetReviews(), Has.Count.EqualTo(3));

            var revToDelete = _reviews.Find(r => r.Id == 2);

            revRepo.DeleteReview(revToDelete);

            Assert.That(revRepo.GetReviews(), Has.Count.EqualTo(2));
            Assert.That(revRepo.GetReviewById(1), Is.Not.Null);
            Assert.That(revRepo.GetReviewById(2), Is.Null);
            Assert.That(revRepo.GetReviewById(3), Is.Not.Null);
        }
        
        [Test]
        public void TestGetReviews()
        {
            ReviewRepository revRepo = new ReviewRepository();

            foreach (var rev in _reviews)
            {
                revRepo.AddReview(rev);
            }

            Assert.That(revRepo.GetReviews(), Has.Count.EqualTo(3));
        }

        [Test]
        public void TestGetReviewsById()
        {
            ReviewRepository revRepo = new ReviewRepository();

            Assert.That(revRepo.GetReviewById(1), Is.Null);

            foreach (var rev in _reviews)
            {
                revRepo.AddReview(rev);
            }

            var user = _users.Find(u => u.Name == "Bill Fisher");

            Assert.That(revRepo.GetReviewById(1), Is.Not.Null);
            Assert.That(revRepo.GetReviewById(1).Id, Is.EqualTo(1));
            Assert.That(revRepo.GetReviewById(1).FoodRating, Is.EqualTo(4));
            Assert.That(revRepo.GetReviewById(1).CleanlinessRating, Is.EqualTo(5));
            Assert.That(revRepo.GetReviewById(1).ServiceRating, Is.EqualTo(4));
            Assert.That(revRepo.GetReviewById(1).AmbianceRating, Is.EqualTo(4));
            Assert.That(revRepo.GetReviewById(1).OverallRating, Is.EqualTo(5));
            Assert.That(revRepo.GetReviewById(1).Review, Is.EqualTo("Eat here all the time"));
            Assert.That(revRepo.GetReviewById(1).SubmittingUser, Is.EqualTo(user));
        }
        
        [Test]
        public void TestGetReviewsByUser()
        {
            List<IReviewModel> reviews = new List<IReviewModel>();

            ReviewRepository revRepo = new ReviewRepository();

            // If I were using a UserRepository for the source of users,
            // I would use the ID for the Find predicate.  I'm not testing
            // the UserRepository in this [Test] so I'll just find one by 
            // Name.
            var user = _users.Find(u => u.Name == "Bill Fisher"); 

            Assert.That(reviews, Has.Count.EqualTo(0));

            foreach (var rev in _reviews)
            {
                revRepo.AddReview(rev);
            }
            
            foreach (var review in revRepo.GetReviewsByUser(user))
            {
                reviews.Add(review);
            }

            Assert.That(reviews, Has.Count.EqualTo(2));
        }

        [Test]
        public void TestAddUser()
        {
            UserRepository userRepo = new UserRepository();

            Assert.That(userRepo.HasData, Is.False);
            Assert.That(userRepo.GetUserById(1), Is.Null);

            foreach (var user in _users)
            {
                userRepo.AddUser(user);
            }

            Assert.That(userRepo.HasData, Is.True);
            Assert.That(userRepo.GetUserById(1), Is.Not.Null);
        }
        
        [Test]
        public void TestGetUserById()
        {
            UserRepository userRepo = new UserRepository();

            Assert.That(userRepo.HasData, Is.False);
            Assert.That(userRepo.GetUserById(1), Is.Null);

            foreach (var user in _users)
            {
                userRepo.AddUser(user);
            }

            Assert.That(userRepo.HasData, Is.True);
            Assert.That(userRepo.GetUserById(1), Is.Not.Null);
            Assert.That(userRepo.GetUserById(1).Id, Is.EqualTo(1));
            Assert.That(userRepo.GetUserById(1).Name, Is.EqualTo("Bill Fisher"));
        }
    }
}