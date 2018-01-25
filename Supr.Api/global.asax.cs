using Supr.Model.Entity;
using System.Data.Entity;
using System.Web.Http;

namespace Supr.Api {
	public class WebApiApplication : System.Web.HttpApplication {
		protected void Application_Start() {
			Database.SetInitializer<SuprContext>( null );
			GlobalConfiguration.Configure( WebApiConfig.Register );
		}
	}
}
