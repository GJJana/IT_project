using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnimalTinder.Startup))]
namespace AnimalTinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
