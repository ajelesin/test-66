namespace MvcApp.Models.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;

    public class FakeRepository : IRepository
    {
        private readonly List<Order> _orders;
        private readonly List<Trade> _tradeHistory;

        public FakeRepository()
        {
            _orders = new List<Order>();
            _tradeHistory = new List<Trade>();
        }

        public IQueryable<Order> GetOrders()
        {
            return _orders.AsQueryable();
        }

        public IQueryable<Trade> GetTradeHistory()
        {
            return _tradeHistory.AsQueryable();
        }

        public void AddOrder(Order order)
        {
            order.Id = _orders.Count;
            _orders.Add(order);
        }

        public void AddTradeItem(Trade trade)
        {
            trade.Id = _tradeHistory.Count;
            _tradeHistory.Add(trade);
        }

        public void UpdateOrder(Order order)
        {
            var foundIndex = _orders.FindIndex(o => o.Id == order.Id);
            if (foundIndex >= 0)
            {
                _orders[foundIndex] = order;
            }
        }

        public void SaveChanges()
        {
            // nothing to do here
        }

        public static FakeRepository SomeData
        {
            get
            {
                var repo = new FakeRepository();
                repo._orders.AddRange(new List<Order>
                {
                    new Order {Id = 0, TotalAmount = 5, AvailableAmount = 5, Price = 20, OrderType = OrderType.Sell, Date = DateTime.Now},
                    new Order {Id = 1, TotalAmount = 2, AvailableAmount = 2, Price = 21, OrderType = OrderType.Sell, Date = DateTime.Now},
                    new Order {Id = 2, TotalAmount = 1, AvailableAmount = 1, Price = 25.5m, OrderType = OrderType.Sell, Date = DateTime.Now},
                    new Order {Id = 3, TotalAmount = 4,AvailableAmount = 4,  Price = 30, OrderType = OrderType.Sell, Date = DateTime.Now},
                    new Order {Id = 4, TotalAmount = 1, AvailableAmount = 1, Price = 45, OrderType = OrderType.Sell, Date = DateTime.Now},
                    new Order {Id = 5, TotalAmount = 5,AvailableAmount = 5,  Price = 19, OrderType = OrderType.Buy, Date = DateTime.Now},
                    new Order {Id = 6, TotalAmount = 2, AvailableAmount = 2, Price = 18, OrderType = OrderType.Buy, Date = DateTime.Now},
                    new Order {Id = 7, TotalAmount = 1, AvailableAmount = 1, Price = 15, OrderType = OrderType.Buy, Date = DateTime.Now},
                    new Order {Id = 8, TotalAmount = 50, AvailableAmount = 50, Price = 10, OrderType = OrderType.Buy, Date = DateTime.Now},
                    new Order {Id = 9, TotalAmount = 2,AvailableAmount = 2,  Price = 9, OrderType = OrderType.Buy, Date = DateTime.Now},
                });
                repo._tradeHistory.Add(new Trade{Id = 0,Price = 20,Amount = 3,
                        BuyOrder = new Order {TotalAmount = 3, Price = 20, Date = DateTime.Now},
                        SellOrder = new Order {TotalAmount = 3, Price = 20, Date = DateTime.Now},
                        TradeDate = DateTime.Now
                    });
                return repo;
            }
        }
    }
}