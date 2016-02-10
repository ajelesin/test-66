using System.Linq;
using WebApp.Models.Domain;

namespace WebApp.Models.DataAccess
{
    public interface IRepository
    {
        IQueryable<BaseOrder> GetOrders();
        IQueryable<TradeItem> GetTradeHistory();
        void AddOrder(BaseOrder order);
        void AddTradeItem(TradeItem trade);
        void UpdateOrder(BaseOrder order);
    }
}