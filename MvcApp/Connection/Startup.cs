﻿using Microsoft.Owin;

[assembly: OwinStartup(typeof(MvcApp.Connection.Startup))]
namespace MvcApp.Connection
{
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}