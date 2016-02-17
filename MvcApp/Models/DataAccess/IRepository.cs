namespace MvcApp.Models.DataAccess
{
    using System.Linq;
    using Domain;

    public interface IRepository
    {
        IQueryable<Order> GetOrders();
        IQueryable<Trade> GetTradeHistory();
        void AddOrder(Order order);
        void AddTradeItem(Trade trade);
        void UpdateOrder(Order order);
        void SaveChanges();
    }
}