using Microsoft.Extensions.DependencyInjection;

namespace RestaurantReview.API.Controllers.Tests
{
	public static class TestServiceProvider
	{
		internal static ServiceProvider GetProvider()
		{
			var baseServices = new TestServices().BaseServices();
			var serviceProvider = baseServices.BuildServiceProvider();
			return serviceProvider;
		}
	}
}