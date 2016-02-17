namespace MvcApp.Connection
{
    using Microsoft.AspNet.SignalR;
    using Models.Domain;

    public class ExchangeConnection : PersistentConnection
    {
        public void SendData(ExchangeService service)
        {
            var tradeList = service.GetTradeHistory();
            var buyList = service.GetBuyOrders();
            var sellList = service.GetSellOrders();

            Connection.Broadcast(new {Trades = tradeList, BuyOrders = buyList, SellOrders = sellList});
        }
    }
}