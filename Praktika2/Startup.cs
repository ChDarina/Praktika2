using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Praktika2.Startup))]
namespace Praktika2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
