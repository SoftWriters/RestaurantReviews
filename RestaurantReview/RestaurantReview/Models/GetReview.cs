using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RestaurantReview.Models
{
    public class GetReview
    {
        [Required]
        public int ReviewId { get; set; }

        [Required]
        public Restaurant Restaurant { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string ReviewText { get; set; }

        public bool ValidateUserNameFormat()
        {
            MatchCollection matches;
            Regex defaultRegex = new Regex(@"^[A-Za-z]{4, 15}+$");
            matches = defaultRegex.Matches(this.User.UserName);

            return matches.Count == 1;
        }

        public bool IsValidId()
        {
            return this.ReviewId > 0;
        }

        public bool IsValidReviewText()
        {
            return this.ReviewText.Split(" ").Length > 1;
        }
    }
}