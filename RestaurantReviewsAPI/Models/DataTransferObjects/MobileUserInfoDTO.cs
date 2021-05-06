using System.ComponentModel.DataAnnotations;

namespace RestaurantReviewsAPI.Models.DataTransferObjects
{
    public class MobileUserInfoDTO
    {
        public long UserId { get; set; }
        public string Name { get; set; }
    }
}
