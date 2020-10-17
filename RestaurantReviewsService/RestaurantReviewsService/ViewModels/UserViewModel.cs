namespace RestaurantReviewsService.ViewModels
{
    public sealed class UserViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Alias { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int StateIdRef { get; set; }

    }
}