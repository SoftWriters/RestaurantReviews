using RestaurantRating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantRating.Data
{
    public class DummyData
    {
        public static List<Restaurant> getrest()
        {
            List<Restaurant> rest = new List<Restaurant>()
{
new Restaurant() {
RestaurantName = "Cake",
City ="Atlanta"
},
new Restaurant() {
RestaurantName = "Monsoon",
City ="NewYork"
},
new Restaurant() {
RestaurantName = "Welcome",
City ="Madison"
},
new Restaurant() {
RestaurantName = "Subway",
City ="Chicago"
},
new Restaurant() {
RestaurantName = "CornerStone",
City ="Pittsburgh"
},
new Restaurant() {
RestaurantName = "HillStone",
City ="Phoenix"
},
new Restaurant() {
RestaurantName = "FrankBistro",
City ="Chicago"
},
new Restaurant() {
RestaurantName = "BlueMoon",
City ="NewYork"
}


};
            return rest;
        }

        public static List<Users> getuser()
        {
            List<Users> us1 = new List<Users>()
{
new Users() {

NameOfRest = "Subway",
UserName = "Amanda",
Reviews ="Restaurant was good"
},
new Users() {
NameOfRest = "Welcome",
UserName = "Rahul",
Reviews ="I liked their noodles and desert"
},
new Users() {
NameOfRest = "Monsoon",
UserName = "Robert",
Reviews ="The ambience is wonderful"
},
new Users() {
NameOfRest = "HillStone",
UserName = "Riya",
Reviews ="The services were excellent and waiter was amazing"
},
new Users() {
NameOfRest = "BlueMoon",
UserName = "Riya",
Reviews ="Food was worth the bill"
},
new Users() {
NameOfRest = "HardRock",
UserName = "Amanda",
Reviews ="Food was worth the bill"
},
new Users() {
NameOfRest = "Cake",
UserName = "Sam",
Reviews ="The ambience was good"
}



};
            return us1;
        }
    }
}