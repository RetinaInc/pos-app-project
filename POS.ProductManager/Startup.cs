using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POS.ProductManager.Startup))]
namespace POS.ProductManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
