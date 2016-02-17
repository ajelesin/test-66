using Microsoft.Owin;

[assembly: OwinStartup(typeof(MvcApp.Connection.Startup))]

namespace MvcApp.Connection
{
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            // app.MapSignalR<ExchangeConnection>("/exchange");
            app.MapSignalR();
        }
    }
}