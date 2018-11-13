using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Repositories
{
    public static class AppDataLoader
    {
        public static void LoadData(IChainRepository chainRepo, ICityRepository cityRepo, IRestaurantRepository restaurantRepo, IReviewRepository reviewRepo, IUserRepository userRepo)
        {
            var mon = new CityModel(1, "Monroeville, PA");
            var oak = new CityModel(2, "Oakland, PA");
            var mcc = new CityModel(3, "McCandless, PA");
            var asp = new CityModel(4, "Aspinwall, PA");
            var pit = new CityModel(5, "Pittsburgh, PA");
            var rob = new CityModel(6, "Robinson Township, PA");

            var og = new ChainModel(1, "Olive Garden");
            var mm = new ChainModel(2, "Mad Mex");

            var wbf = new UserModel(1, "Bill Fisher", true);
            var joe = new UserModel(2, "Joe Smith", false);

            var mmoak = new RestaurantModel(1, "Mad Mex", oak, mm, "Atwood Street");
            var mmmcc = new RestaurantModel(2, "Mad Mex", mcc, mm, "McKnight Road");
            var mmmon = new RestaurantModel(3, "Mad Mex", mon, mm, "Miracle Mile");
            var ogmon = new RestaurantModel(4, "Olive Garden", mon, og, "Monroeville Mall");
            var ogmcc = new RestaurantModel(5, "Olive Garden", mcc, og, "Ross Park Mall");
            var ogrob = new RestaurantModel(6, "Olive Garden", rob, og, "Fayette Center");
            var corner = new RestaurantModel(7, "Cornerstone", asp, null, "Freeport Road");
            var donp = new RestaurantModel(8, "Don Parmesan's", mcc, null, "McKnight Road");

            if (!cityRepo.HasData())
            {
                cityRepo.AddCity(mon);
                cityRepo.AddCity(oak);
                cityRepo.AddCity(mcc);
                cityRepo.AddCity(asp);
                cityRepo.AddCity(pit);
                cityRepo.AddCity(rob);
            }

            if (!chainRepo.HasData())
            {
                chainRepo.AddChain(og);
                chainRepo.AddChain(mm);
            }

            if (!userRepo.HasData())
            {
                userRepo.AddUser(wbf);
                userRepo.AddUser(joe);
            }

            if (!restaurantRepo.HasData())
            {
                restaurantRepo.AddRestaurant(mmoak);
                restaurantRepo.AddRestaurant(mmmcc);
                restaurantRepo.AddRestaurant(mmmon);
                restaurantRepo.AddRestaurant(ogmon);
                restaurantRepo.AddRestaurant(ogmcc);
                restaurantRepo.AddRestaurant(ogrob);
                restaurantRepo.AddRestaurant(corner);
                restaurantRepo.AddRestaurant(donp);
            }
        }
    }
}