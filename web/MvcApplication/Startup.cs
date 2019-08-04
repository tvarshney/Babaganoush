using Microsoft.Owin;
using MvcApplication;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace MvcApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
