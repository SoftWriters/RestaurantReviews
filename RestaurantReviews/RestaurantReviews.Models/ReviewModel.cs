namespace RestaurantReviews.Models
{
    public class ReviewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RestaurantID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
