using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpaceBlog.Startup))]
namespace SpaceBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
