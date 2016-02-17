using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Advertisement.Startup))]
namespace Advertisement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
