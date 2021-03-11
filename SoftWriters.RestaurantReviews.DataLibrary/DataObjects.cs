using System;
using System.Runtime.Serialization;

namespace SoftWriters.RestaurantReviews.DataLibrary
{
    [DataContract]
    public class User
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public Address Address { get; set; }

        public User()
        { }

        public User(Guid id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }

    [DataContract]
    public class Address
    {
        [DataMember] public string StreetAddress { get; set; }
        [DataMember] public string PostalCode { get; set; }
        [DataMember] public string City { get; set; }
        [DataMember] public string StateCode { get; set; }
        [DataMember] public string Country { get; set; }

        public Address()
        { }

        public Address(string street, string postalCode, string city, string stateCode, string country)
        {
            StreetAddress = street;
            PostalCode = postalCode;
            City = city;
            StateCode = stateCode;
            Country = country;
        }
    }

    [DataContract]
    public class Review
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public Guid UserId { get; set; }
        [DataMember] public Guid RestaurantId { get; set; }
        [DataMember] public int OverallRating { get; set; }
        [DataMember] public int FoodRating { get; set; }
        [DataMember] public int ServiceRating { get; set; }
        [DataMember] public int CostRating { get; set; }
        [DataMember] public string Comments { get; set; }

        public Review()
        { }

        public Review(Guid id, Guid userId, Guid restaurantId, int overallRating, int foodRating, int serviceRating, int costRating, string comments)
        {
            Id = id;
            UserId = userId;
            RestaurantId = restaurantId;
            OverallRating = overallRating;
            FoodRating = foodRating;
            ServiceRating = serviceRating;
            CostRating = costRating;
            Comments = comments;
        }
    }

    [DataContract]
    public class Restaurant
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public Address Address { get; set; }

        public Restaurant()
        { }

        public Restaurant(Guid id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
