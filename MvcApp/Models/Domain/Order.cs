namespace MvcApp.Models.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Price per cake")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Price must be more than zero")]
        public decimal Price { get; set; }

        [Display(Name = "Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be more than zero")]
        public int TotalAmount { get; set; } // всего было поставлено на покупку/продажу

        public int AvailableAmount { get; set; } // доступно на текущий момент для покупки/продажи

        public string Note { get; set; }

        public DateTime Date { get; set; }

        [EnumDataType(typeof(OrderType))]
        public OrderType OrderType { get; set; }
    }
}