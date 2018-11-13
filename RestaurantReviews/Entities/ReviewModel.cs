namespace Models
{
    public class ReviewModel : IReviewModel
    {
        public ReviewModel(IUserModel submittingUser, RestaurantModel restaurant, int foodRating, int cleanRating, int serviceRating, int ambianceRating, int overallRating, string reviewText)
        {
            SubmittingUser = submittingUser;
            Restaurant = restaurant;
            FoodRating = foodRating;
            CleanlinessRating = cleanRating;
            ServiceRating = serviceRating;
            AmbianceRating = ambianceRating;
            OverallRating = overallRating;
            Review = reviewText;
        }

        public int Id
        { get; set; }

        public IUserModel SubmittingUser
        { get; set; }

        public IRestaurantModel Restaurant
        { get; set; }

        public int FoodRating
        { get; set; }

        public int CleanlinessRating
        { get; set; }

        public int ServiceRating
        { get; set; }

        public int AmbianceRating
        { get; set; }

        public int OverallRating
        { get; set; }

        public string Review
        { get; set; }
    }
}