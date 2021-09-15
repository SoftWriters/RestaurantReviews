using System.ServiceProcess;

namespace SoftWriters.RestaurantReviews.Service
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
