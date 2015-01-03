using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nmct.ba.cashless.WebApp.Startup))]
namespace nmct.ba.cashless.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
