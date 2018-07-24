using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecordRetentionApp.Startup))]
namespace RecordRetentionApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
