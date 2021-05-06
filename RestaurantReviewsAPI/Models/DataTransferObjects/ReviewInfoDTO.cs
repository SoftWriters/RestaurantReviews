using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewsAPI.Models.DataTransferObjects
{
    public class ReviewInfoDTO
    {
        public long ReviewId { get; set; }
        public RatingInfoDTO RatingInfo { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDT { get; set; }
        public MobileUserInfoDTO UserInfo { get; set; }
        public RestaurantInfoDTO RestaurantInfo { get; set; }    
    }
}
