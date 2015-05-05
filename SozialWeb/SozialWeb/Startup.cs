using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SozialWeb.Startup))]
namespace SozialWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
