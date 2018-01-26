using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace RestaurantReviews.Models
{
	//Make abstract - don't need an instance of this. Not an interface, which defines a 'can do a' relationship
	//Abstract or base classes allow for 'is a' relationships which are more focused on pure inheritance.
	public abstract class ViewModelFactory
		//While odd, I very much am a fan of enumerating one parameter per line as it can make editing easier when spanning them out
		//I wouldn't normally do it in this case but am just demonstrating for the code sample, thought process behind it
	<
		TOriginalModelType,
		TResultantViewModel, 
		TControllerToLinkTo
	>
	where TResultantViewModel : ViewModel, new()
	{
		//Put this down here to be shared for each class that we are doing viewmodels for
		//Reduced code duplication
		protected UrlHelper UrlHelper { get; set; }

		//Don't need these visible outside of the factory or it's descendants
		protected string ControllerLinkName { get; set; }

		public ViewModelFactory(HttpRequestMessage httpRequestMessage)
		{
			UrlHelper = new UrlHelper(httpRequestMessage);

			//Make your links here via reflection/generic stuff
			ControllerLinkName = typeof(TControllerToLinkTo).Name.Replace("Controller", "").ToLower();
		}

		public abstract TResultantViewModel Map(TOriginalModelType model);

		//Saw that this could go down here - much less code duplication which is the friend of effiency and flow
		public IEnumerable<TResultantViewModel> MapMany(IEnumerable<TOriginalModelType> models)
		{
			foreach (var model in models)
			{
				//Using yield here instead of a placeholder variable to return
				yield return Map(model);
			}
		}

		protected TResultantViewModel CoreMappingLogic(TOriginalModelType model)
		{
			//I'm not a fan of doing var - are you? Can explain why, as well. 
			TResultantViewModel viewModel = new TResultantViewModel();

			//I'm a fan of function pointers to reuse logic for middle-modification code paths
			MappingLogic(model, viewModel);

			viewModel.Href = UrlHelper.Link("DefaultApi", new { controller = ControllerLinkName, id = viewModel.Id });

			return viewModel;
		}

		//Made crystal clear to developers that this needs done and also to use this 'slot' instead of an inline method. Help the next developer to understand.
		protected abstract void MappingLogic(TOriginalModelType model, TResultantViewModel viewModel);
	}
}