using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeweyLMS.Startup))]
namespace DeweyLMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
