using System;
using System.ServiceModel.Web;
using SoftWriters.RestaurantReviews.WebApi;

namespace ServiceConsole
{
    // For testing and debugging

    class Program
    {
        static void Main(string[] args)
        {
            var serviceHost = new WebServiceHost(typeof(ReviewApi));
            serviceHost.Open();

            Console.WriteLine("HTTP Service is running. Press any key to quit...");
            Console.ReadKey();
            serviceHost.Close();
        }
    }
}
