namespace MvcApp.Models.DataAccess
{
    using System.Data.Entity;
    using Domain;

    public class ExchangeContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Trade> TadeItems { get; set; } 
    }
}