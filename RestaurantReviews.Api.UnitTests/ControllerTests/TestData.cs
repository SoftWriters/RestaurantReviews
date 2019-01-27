using System;
using System.Collections.Generic;
using RestaurantReviews.Api.Models;

namespace RestaurantReviews.Api.UnitTests.ControllerTests
{
    public static class TestData
    {
        public static readonly Restaurant McDonalds = new Restaurant
        {
            Id = 123,
            Name = "McDonald's",
            Description = "The Golden Arches",
            City = "Pittsburgh",
            State = "PA"
        };

        public static readonly Restaurant Wendys = new Restaurant
        {
            Id = 456,
            Name = "Wendy's",
            Description = "Dave's Place",
            City = "Pittsburgh",
            State = "PA"
        };
        
        public static readonly List<Restaurant> RestaurantTable =
            new List<Restaurant> { McDonalds, Wendys };

        public static readonly Review McDonaldsReview = new Review
        {
            Id = 1234,
            RestaurantId = McDonalds.Id,
            ReviewerEmail = "jane@smith.xyz",
            RatingStars = 4.5m,
            Comments = "Great Nuggets!",
            ReviewedOn = DateTimeOffset.Now
        };
        
        public static readonly Review WendysReview = new Review
        {
            Id = 5678,
            RestaurantId = Wendys.Id,
            ReviewerEmail = "fred@jones.xyz",
            RatingStars = 3.2m,
            Comments = "Frosty was yummy.",
            ReviewedOn = DateTimeOffset.Now
        };
    }
}