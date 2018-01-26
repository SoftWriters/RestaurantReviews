namespace RestaurantReviews.Models
{
	public class ReviewViewModel : ViewModel
	{
		public string RestaurantName { get; set; }
		public int Score { get; set; }
		public string Comments { get; set; }
		public string User { get; set; }
		//Notice that active was left out - not a relevant user item
	}
}