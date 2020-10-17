namespace RestaurantReviewService.Entities
{
    public sealed class UserReview : EntityModelBase
    {
        public int Id { get; set; }

        public int UserIdRef { get; set; }

        public int RestaurantIdRef { get; set; }

        public string Title { get; set; }

        public string Comments { get; set; }

        public int RatingsRef { get; set; }

        public int RatingsLevel { get; set; }

        public string RatingsDescription { get; set; }
    }
}
