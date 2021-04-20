using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestaurantReview.Page.Pages
{
	public class AddRestaurantModel : PageModel
	{
		public string City { get; set; }

		public void OnGet(string city)
		{
			City = city ?? "Pittsburgh";
		}
	}
}