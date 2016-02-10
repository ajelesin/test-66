using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.Domain;

namespace WebApp.Models.DataAccess
{
    public class FakeRepository : IRepository
    {
        private static readonly List<BaseOrder> Orders = new List<BaseOrder>
        {
            new BaseOrder {Id = 0, TotalAmount = 5, AvailableAmount = 5, Price = 20, OrderType = OrderType.Sell, Date = DateTime.Now},
            new BaseOrder {Id = 1, TotalAmount = 2, AvailableAmount = 2, Price = 21, OrderType = OrderType.Sell, Date = DateTime.Now},
            new BaseOrder {Id = 2, TotalAmount = 1, AvailableAmount = 1, Price = (decimal) 25.5, OrderType = OrderType.Sell, Date = DateTime.Now},
            new BaseOrder {Id = 3, TotalAmount = 4,AvailableAmount = 4,  Price = 30, OrderType = OrderType.Sell, Date = DateTime.Now},
            new BaseOrder {Id = 4, TotalAmount = 1, AvailableAmount = 1, Price = 45, OrderType = OrderType.Sell, Date = DateTime.Now},
            new BaseOrder {Id = 5, TotalAmount = 5,AvailableAmount = 5,  Price = 19, OrderType = OrderType.Buy, Date = DateTime.Now},
            new BaseOrder {Id = 6, TotalAmount = 2, AvailableAmount = 2, Price = 18, OrderType = OrderType.Buy, Date = DateTime.Now},
            new BaseOrder {Id = 7, TotalAmount = 1, AvailableAmount = 1, Price = 15, OrderType = OrderType.Buy, Date = DateTime.Now},
            new BaseOrder {Id = 8, TotalAmount = 50, AvailableAmount = 50, Price = 10, OrderType = OrderType.Buy, Date = DateTime.Now},
            new BaseOrder {Id = 9, TotalAmount = 2,AvailableAmount = 2,  Price = 9, OrderType = OrderType.Buy, Date = DateTime.Now},
        };

        private  static readonly List<TradeItem> TradeHistory = new List<TradeItem>
        { 
            new TradeItem
            {
                Id = 0,
                Price = 20,
                Amount = 3,
                BuyOrder = new BaseOrder{TotalAmount = 3, Price = 20, Date = DateTime.Now},
                SellOrder = new BaseOrder{TotalAmount = 3, Price = 20, Date = DateTime.Now},
                TradeDate = DateTime.Now
            }
        }; 

        public IQueryable<BaseOrder> GetOrders()
        {
            return Orders.AsQueryable();
        }

        public IQueryable<TradeItem> GetTradeHistory()
        {
            return TradeHistory.AsQueryable();
        }

        public void AddOrder(BaseOrder order)
        {
            order.Id = Orders.Count;
            Orders.Add(order);
        }

        public void AddTradeItem(TradeItem trade)
        {
            trade.Id = TradeHistory.Count;
            TradeHistory.Add(trade);
        }

        public void UpdateOrder(BaseOrder order)
        {
            var foundIndex = Orders.FindIndex(o => o.Id == order.Id);
            if (foundIndex >= 0)
            {
                Orders[foundIndex] = order;
            }
        }
    }
}