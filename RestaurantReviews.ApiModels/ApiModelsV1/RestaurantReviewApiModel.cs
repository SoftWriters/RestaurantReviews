using System;

namespace RestaurantReviews.ApiModels.ApiModelsV1
{
    public class RestaurantReviewApiModel
    {
        public int RestaurantReviewApiId { get; set; }
        public int RestaurantApiId { get; set; }
        public int ReviewerApiId { get; set; }
        public DateTime ReviewDate { get; set; }
        public byte NumberOfStars { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}