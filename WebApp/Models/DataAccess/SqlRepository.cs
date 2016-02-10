using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using WebApp.Models.Domain;

namespace WebApp.Models.DataAccess
{
    public class SqlRepository : IRepository, IDisposable
    {
        private readonly ExchangeContext _context;

        public SqlRepository()
        {
            _context = new ExchangeContext();
        }

        public IQueryable<BaseOrder> GetOrders()
        {
            return _context.Orders;
        }

        public IQueryable<TradeItem> GetTradeHistory()
        {
            return _context.TadeItems
                .Include("SellOrder")
                .Include("BuyOrder");
        }

        public void AddOrder(BaseOrder order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddTradeItem(TradeItem trade)
        {
            _context.TadeItems.Add(trade);
            _context.SaveChanges();
        }

        public void UpdateOrder(BaseOrder order)
        {
            _context.Entry(order).CurrentValues.SetValues(order);
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