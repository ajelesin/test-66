using System.Data.Entity;
using WebApp.Models.Domain;

namespace WebApp.Models.DataAccess
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext()
            : base("DefaultConnection")
        {
            // nothing to do here
        }

        public DbSet<BaseOrder> Orders { get; set; }
        public DbSet<TradeItem> TadeItems { get; set; } 
    }
}