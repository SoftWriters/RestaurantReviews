using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewsAPI.Models.DataTransferObjects
{
    public class RestaurantInfoDTO
    {
        public long RestaurantId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDT { get; set; }
        public CityInfoDTO CityInfo { get; set; }
    }
}
