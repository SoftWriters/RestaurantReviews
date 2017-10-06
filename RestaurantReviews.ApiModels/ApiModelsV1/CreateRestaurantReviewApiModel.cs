namespace RestaurantReviews.ApiModels.ApiModelsV1
{
    public class CreateRestaurantReviewApiModel
    {
        public int RestaurantApiId { get; set; }
        public string ReviewerEmail { get; set; }
        public byte NumberOfStars { get; set; }
        public string Text { get; set; }
    }
}