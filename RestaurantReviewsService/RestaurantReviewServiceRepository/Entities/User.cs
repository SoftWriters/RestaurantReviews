namespace RestaurantReviewService.Entities
{
    public sealed class User : EntityModelBase
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Alias { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int StateIdRef { get; set; }
    }
}
