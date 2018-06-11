using System;
using Telerik.ApiTesting.Framework;
using Telerik.ApiTesting.Framework.Abstractions;

namespace API_SmokeTest.Restaurant
{
	// It is mandatory for each test class to inherit ApiTestBase class as shown below (this is set by default).
    public class Add : ApiTestBase
    {				
		public void SetRandomRestaurantName()
		{
			System.Random random = new  System.Random();
			int randomRestaurantName = random.Next(1, 65535);
			this.Context.SetValue("restaurantName", "Restaurant " + randomRestaurantName.ToString(), 1);
		}
	}
}