namespace Models
{
    public interface IReviewModel
    {
        int Id { get; set; }
        IUserModel SubmittingUser { get; set; }
        IRestaurantModel Restaurant { get; set; }
        int FoodRating { get; set; }
        int CleanlinessRating { get; set; }
        int ServiceRating { get; set; }
        int AmbianceRating { get; set; }
        int OverallRating { get; set; }
        string Review { get; set; }

        // And over here, we can put some fields!  Photos, date of review, etc.
    }
}
