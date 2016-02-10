using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.DataAccess;

namespace WebApp.Models.Domain
{
    public class ExchangeService
    {
        private readonly IRepository _repository;

        public ExchangeService(IRepository repository)
        {
            _repository = repository;
        }

        public Result<List<BaseOrder>> GetSellOrders()
        {
            try
            {
                var sellOrders = _repository.GetOrders()
                    .Where(o => o.OrderType == OrderType.Sell && o.AvailableAmount > 0)
                    .OrderByDescending(o => o.Date)
                    .ToList();
                return Result.Ok(sellOrders);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<BaseOrder>>("Error: " + ex);
            }
        }

        public Result<List<BaseOrder>> GetBuyOrders()
        {
            try
            {
                var buyOrders = _repository.GetOrders()
                    .Where(o => o.OrderType == OrderType.Buy && o.AvailableAmount > 0)
                    .OrderByDescending(o => o.Date)
                    .ToList();
                return Result.Ok(buyOrders);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<BaseOrder>>("Error: " + ex);
            }
        }

        public Result<List<TradeItem>> GetTradeHistory()
        {
            try
            {
                var history = _repository.GetTradeHistory()
                    .OrderByDescending(o => o.TradeDate)
                    .ToList();
                return Result.Ok(history);
            }
            catch (Exception ex)
            {
                return Result.Fail<List<TradeItem>>("Error: " + ex);
            }
        }

        public Result ProcessOrder(BaseOrder order)
        {
            try
            {
                if (order.TotalAmount <= 0)
                    throw new ArgumentException("Order's amount must be more than 0!");
                if (order.Price <= 0)
                    throw new ArgumentException("Order's price must be more than 0!");

                order.Date = DateTime.Now;
                order.AvailableAmount = order.TotalAmount;
                
                MakeDeals(order);

                _repository.AddOrder(order);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail("Error: " + ex);
            }
        }

        private void MakeDeals(BaseOrder order)
        {
            if (order.OrderType == OrderType.Sell)
            {
                var rightOrders = _repository.GetOrders()
                    .Where(o => o.AvailableAmount > 0 && o.Price <= order.Price && o.OrderType == OrderType.Buy)
                    .OrderBy(o => o.Date)
                    .ToList();
                if (rightOrders.Count == 0)
                    return;

                foreach (var rightOrder in rightOrders)
                {
                    if (order.AvailableAmount <= 0) break;

                    var amount = Math.Min(order.AvailableAmount, rightOrder.AvailableAmount);
                    order.AvailableAmount -= amount;
                    rightOrder.AvailableAmount -= amount;

                    var tradeItem = new TradeItem
                    {
                        Amount = amount,
                        BuyOrder = order,
                        SellOrder = rightOrder,
                        Price = order.Price,
                        TradeDate = DateTime.Now
                    };
                    _repository.AddTradeItem(tradeItem);
                    _repository.UpdateOrder(rightOrder);
                }
            }

            if (order.OrderType == OrderType.Buy)
            {
                var rightOrders = _repository.GetOrders()
                    .Where(o => o.AvailableAmount > 0 && o.Price >= order.Price && o.OrderType == OrderType.Sell)
                    .OrderBy(o => o.Date)
                    .ToList();
                if (rightOrders.Count == 0)
                    return;

                foreach (var rightOrder in rightOrders)
                {
                    if (order.AvailableAmount <= 0) break;

                    var amount = Math.Min(order.AvailableAmount, rightOrder.AvailableAmount);
                    order.AvailableAmount -= amount;
                    rightOrder.AvailableAmount -= amount;

                    var tradeItem = new TradeItem
                    {
                        Amount = amount,
                        BuyOrder = rightOrder,
                        SellOrder = order,
                        Price = rightOrder.Price,
                        TradeDate = DateTime.Now
                    };
                    _repository.AddTradeItem(tradeItem);
                    _repository.UpdateOrder(rightOrder);
                }
            }
        }

    }
}