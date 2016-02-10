using System;

namespace WebApp.Models.Domain
{
    public class BaseOrder
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int TotalAmount { get; set; } // всего было поставлено на покупку/продажу
        public int AvailableAmount { get; set; } // доступно на текущий момент для покупки/продажи
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public OrderType OrderType { get; set; }
    }
}