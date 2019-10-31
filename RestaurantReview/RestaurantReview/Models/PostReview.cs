using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RestaurantReview.Models
{
    public class PostReview
    {
        
        public int? ReviewId { get; set; }

        [Required]
        public int? RestaurantId { get; set; }

        [Required]
        public int UserId{ get; set; }

        [Required]
        public string ReviewText { get; set; }

        //public bool ValidateUserNameFormat()
        //{
        //    MatchCollection matches;
        //    Regex defaultRegex = new Regex(@"^[A-Za-z]{4, 15}+$");
        //    matches = defaultRegex.Matches(this.UserName);

        //    return matches.Count == 1;
        //}
    }
}