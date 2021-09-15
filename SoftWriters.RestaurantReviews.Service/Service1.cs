using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using SoftWriters.RestaurantReviews.WebApi;

namespace SoftWriters.RestaurantReviews.Service
{
    public partial class Service1 : ServiceBase
    {
        private readonly ServiceHost _serviceHost;

        public Service1()
        {
            InitializeComponent();
            _serviceHost = new WebServiceHost(typeof(ReviewApi));
        }

        protected override void OnStart(string[] args)
        {
            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            _serviceHost?.Close();
        }
    }
}
