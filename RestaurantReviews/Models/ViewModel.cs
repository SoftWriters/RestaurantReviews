namespace RestaurantReviews.Models
{
	public class ViewModel
	{
		//Here for reduce of duplication as well as use in View Model Factory
		public int? Id { get; set; }
		public string Href { get; set; }
		public bool Active { get; set; } = true;
	}
}