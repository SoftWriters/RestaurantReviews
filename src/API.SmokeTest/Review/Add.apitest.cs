using System;
using Telerik.ApiTesting.Framework;
using Telerik.ApiTesting.Framework.Abstractions;

namespace API_SmokeTest.Review
{
	// It is mandatory for each test class to inherit ApiTestBase class as shown below (this is set by default).
    public class Add : ApiTestBase
    {				
		public void SetReviewedDateTime()
		{
			this.Context.SetValue("reviewedDateTime", DateTime.Now.ToLongDateString());
		}
	}
}