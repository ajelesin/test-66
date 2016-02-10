using System;

namespace WebApp.Models.Domain
{
    public class TradeItem
    {
        public int Id { get; set; }
        public BaseOrder SellOrder { get; set; }
        public BaseOrder BuyOrder { get; set; }
        public DateTime TradeDate { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}