using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestaurantReview.Page.Pages
{
	public class ReviewModel : PageModel
	{
		public string Id { get; set; }

		public void OnGet(string id)
		{
			Id = id;
		}
	}
}