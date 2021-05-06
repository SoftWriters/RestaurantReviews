using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewsAPI.Models.DataTransferObjects
{
    public class CityInfoDTO
    {
        public long CityId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
    }
}