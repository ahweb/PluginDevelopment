using System.Web.Http;
using Microsoft.Owin;
using Owin;
using PluginDevelopment;

[assembly: OwinStartup(typeof(Startup))]


namespace PluginDevelopment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
          //  app.UseWebApi(configuration);
        }
    }
}