using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ruanmou.Homework.MVC.Startup))]
namespace Ruanmou.Homework.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
