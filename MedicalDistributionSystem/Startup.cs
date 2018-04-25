using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicalDistributionSystem.Startup))]
namespace MedicalDistributionSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
