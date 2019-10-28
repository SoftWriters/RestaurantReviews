using System.Text.RegularExpressions;

namespace RestaurantReview.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public Restaurant Restaurant { get; set; }
        public User User { get; set; }
        public string ReviewText { get; set; }
<<<<<<< HEAD

=======
>>>>>>> 0e003586d4895633c82957f8d50d4fc4e29eed18
        public bool ValidateUserNameFormat()
        {
            MatchCollection matches;
            Regex defaultRegex = new Regex(@"^[A-Za-z]{4, 15}+$");
            matches = defaultRegex.Matches(this.User.UserName);
<<<<<<< HEAD

=======
           
>>>>>>> 0e003586d4895633c82957f8d50d4fc4e29eed18
            return matches.Count == 1;
        }

        //public bool ValidateName()
        //{
        //    return this.Name.Length < 40 && this.Name.Length >= 1;
        //}
    }
}