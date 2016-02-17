namespace MvcApp.Controllers
{
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using Models;
    using Models.DataAccess;
    using Models.Domain;

    public class HomeController : Controller
    {
        private readonly ExchangeService _service;

        public HomeController()
        {
            var repository = FakeRepository.SomeData;
            _service = new ExchangeService(repository);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _service.ProcessOrder(order);
            }
            return PartialView("CreateOrder", /*order*/new Order { OrderType = order.OrderType });
        }

        [HttpGet]
        public ActionResult CreateOrder(OrderType orderType)
        {
            var order = new Order {OrderType = orderType};
            return PartialView("CreateOrder", order);
        }

        public ActionResult BuyOrdersListView()
        {
            var list = _service.GetBuyOrders();
            return PartialView("ListOrders", list);
        }

        public ActionResult SellOrdersListView()
        {
            var list = _service.GetSellOrders();
            return PartialView("ListOrders", list);
        }

        public ActionResult TradesHistoryView()
        {
            var list = _service.GetTradeHistory();
            return PartialView("TradesHistoryView", list);
        }

    }
}
