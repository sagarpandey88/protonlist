using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProtonList.Web.Startup))]
namespace ProtonList.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
