using Microsoft.Owin;
using Owin;

[assembly: OwinStartup( typeof( Supr.Api.Startup ) )]

namespace Supr.Api {
	public partial class Startup {
		public void Configuration( IAppBuilder app ) {
			ConfigureAuth( app );
		}
	}
}
