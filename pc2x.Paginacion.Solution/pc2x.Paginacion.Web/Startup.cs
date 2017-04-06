using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pc2x.Paginacion.Web.Startup))]
namespace pc2x.Paginacion.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
