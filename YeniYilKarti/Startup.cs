using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YeniYilKarti.Startup))]
namespace YeniYilKarti
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
