namespace MvcApp.Models.Domain
{
    using System;

    public class Trade
    {
        public int Id { get; set; }
        public Order SellOrder { get; set; }
        public Order BuyOrder { get; set; }
        public DateTime TradeDate { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}