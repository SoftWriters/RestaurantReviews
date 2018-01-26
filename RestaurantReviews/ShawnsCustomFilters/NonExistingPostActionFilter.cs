using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using RestaurantReviews.Models;

namespace RestaurantReviews.ShawnsCustomFilters
{
	//Let's throw in an unnecessary filter for some sprinkling - assumes only one value on post, so that's noted
	//But is it unnecessary? Duplication and error paths cut down to one potential root for this logic 
	[AttributeUsage(AttributeTargets.Method)]
	public class NonExistingPostActionFilter : Attribute, IActionFilter
	{
		public bool AllowMultiple { get; }
		public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			//Let's show some reflection to show job interview understanding
			ViewModel viewModel = actionContext.ActionArguments
				.Single(arg => arg.Value.GetType().IsSubclassOf(typeof(ViewModel)))
				.Value as ViewModel;

			//Don't accept a post with an id
			if (viewModel.Id.HasValue)
				throw new HttpResponseException(HttpStatusCode.BadRequest);

			return continuation();
		}
	}
}