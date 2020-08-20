using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RestaurantReviews.Api.Model
{
    public class User
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public Guid UserId { get; set; }

        public User() { }

        public User(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public string GetKey()
        {
            return Name + ":" + PhoneNumber;
        }
    }
}
