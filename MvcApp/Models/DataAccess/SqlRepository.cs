namespace MvcApp.Models.DataAccess
{
    using System;
    using System.Linq;
    using Domain;

    public class SqlRepository : IRepository, IDisposable
    {
        private readonly ExchangeContext _context;

        public SqlRepository()
        {
            _context = new ExchangeContext();
        }

        public IQueryable<Order> GetOrders()
        {
            return _context.Orders;
        }

        public IQueryable<Trade> GetTradeHistory()
        {
            return _context.TadeItems
                .Include("SellOrder")
                .Include("BuyOrder");
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void AddTradeItem(Trade trade)
        {
            _context.TadeItems.Add(trade);
        }

        public void UpdateOrder(Order order)
        {
            _context.Entry(order).CurrentValues.SetValues(order);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}