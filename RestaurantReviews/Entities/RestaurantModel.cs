﻿namespace Models
{
    public class RestaurantModel : IRestaurantModel
    {
        public RestaurantModel(string name, CityModel city, ChainModel chain, string address)
        {
            // Error checking goes here.  We could also be a little smarter about mapping
            Name = name;
            City = city;
            Chain = chain;
            Address = address;
        }

        public string Address { get; set; }

        public IChainModel Chain { get; set; }

        public ICityModel City { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}