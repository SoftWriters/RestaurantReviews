using System.Text.RegularExpressions;

namespace RestaurantReview.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public bool ValidateCity()
        {
            MatchCollection matches;
            Regex defaultRegex = new Regex(@"^([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$");
            matches = defaultRegex.Matches(this.City);
            return matches.Count == 1;
        }

        public bool ValidateName()
        {
            return this.Name.Length < 40 && this.Name.Length >= 1;
        }
    }
}