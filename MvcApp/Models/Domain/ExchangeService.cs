namespace MvcApp.Models.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess;

    public class ExchangeService
    {
        private readonly IRepository _repository;

        public ExchangeService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Order> GetSellOrders()
        {
            var sellOrders = _repository.GetOrders()
                .Where(o => o.OrderType == OrderType.Sell && o.AvailableAmount > 0)
                .OrderByDescending(o => o.Date)
                .ToList();
            return sellOrders;
        }

        public List<Order> GetBuyOrders()
        {
            var buyOrders = _repository.GetOrders()
                .Where(o => o.OrderType == OrderType.Buy && o.AvailableAmount > 0)
                .OrderByDescending(o => o.Date)
                .ToList();
            return buyOrders;
        }

        public List<Trade> GetTradeHistory()
        {
            var history = _repository.GetTradeHistory()
                .OrderByDescending(o => o.TradeDate)
                .ToList();
            return history;
        }

        public void ProcessOrder(Order order)
        {
            order.Date = DateTime.Now;
            order.AvailableAmount = order.TotalAmount;

            _repository.AddOrder(order);
            MakeTrades(order);
            _repository.SaveChanges();
        }

        private IEnumerable<Order> GetSuitableOrders(Order order)
        {
            if (order.OrderType == OrderType.Sell)
            {
                return _repository.GetOrders()
                    .Where(o => o.AvailableAmount > 0 && o.Price >= order.Price && o.OrderType == OrderType.Buy)
                    .OrderByDescending(o => o.Price)
                    .ThenBy(o => o.Date);
            }

            if (order.OrderType == OrderType.Buy)
            {
                return _repository.GetOrders()
                    .Where(o => o.AvailableAmount > 0 && o.Price <= order.Price && o.OrderType == OrderType.Sell)
                    .OrderBy(o => o.Price)
                    .ThenBy(o => o.Date);
            }

            return Enumerable.Empty<Order>();
        }

        private void MakeTrades(Order newOrder)
        {
            var suitableOrders = GetSuitableOrders(newOrder)
                .ToList();

            if (suitableOrders.Count == 0)
                return;

            foreach (var suitableOrder in suitableOrders)
            {
                if (newOrder.AvailableAmount <= 0)
                    break;

                var tradeAmount = Math.Min(newOrder.AvailableAmount, suitableOrder.AvailableAmount);
                newOrder.AvailableAmount -= tradeAmount;
                suitableOrder.AvailableAmount -= tradeAmount;

                var trade = new Trade
                {
                    Amount = tradeAmount,
                    Price = suitableOrder.Price,
                    TradeDate = DateTime.Now
                };

                if (newOrder.OrderType == OrderType.Buy)
                {
                    trade.BuyOrder = newOrder;
                    trade.SellOrder = suitableOrder;
                }

                if (newOrder.OrderType == OrderType.Sell)
                {
                    trade.SellOrder = newOrder;
                    trade.BuyOrder = suitableOrder;
                }

                _repository.UpdateOrder(suitableOrder);
                _repository.UpdateOrder(newOrder);
                _repository.AddTradeItem(trade);
            }
        }
    }
}