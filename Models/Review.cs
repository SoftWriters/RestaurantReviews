namespace RestaurantReviews.Models
{
    public class Review
    {
        public long ReviewID {get; set;}
        public string UserName {get; set;}
        public string Description {get; set;}
        public virtual Restaurant Restaurant {get; set;}
    }
}