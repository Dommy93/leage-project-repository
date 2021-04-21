using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WillThisWork.Startup))]
namespace WillThisWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
