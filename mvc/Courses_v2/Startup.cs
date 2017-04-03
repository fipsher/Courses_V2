using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Courses_v2.Startup))]
namespace Courses_v2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
