namespace MvcApp.Connection
{
    using Microsoft.AspNet.SignalR;

    public class ExchangeHub : Hub
    {
        public void SendData()
        {
            Clients.All.refresh();
        }
    }
}