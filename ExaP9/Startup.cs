using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExaP9.Startup))]
namespace ExaP9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
