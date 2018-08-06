using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JiraNemonicIntegration.Startup))]
namespace JiraNemonicIntegration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
