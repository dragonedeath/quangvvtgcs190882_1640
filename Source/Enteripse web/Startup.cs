using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Enteripse_web.Startup))]
namespace Enteripse_web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
