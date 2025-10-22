using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CiteTrustApp.Startup))]
namespace CiteTrustApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
