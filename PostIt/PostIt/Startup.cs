using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PostIt.Startup))]
namespace PostIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
