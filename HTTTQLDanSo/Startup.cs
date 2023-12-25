using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HTTTQLDanSo.Startup))]

namespace HTTTQLDanSo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigAutofac(app);
            ConfigureAuth(app);
        }
    }
}