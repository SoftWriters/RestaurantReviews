﻿namespace RestaurantReviews.Models
{
    public class RestaurantRequest
    {
        public long RestaurantId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
