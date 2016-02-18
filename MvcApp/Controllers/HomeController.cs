namespace MvcApp.Controllers
{
    using System.Web.Mvc;
    using Connection;
    using Microsoft.AspNet.SignalR;
    using Models;
    using Models.DataAccess;
    using Models.Domain;

    public class HomeController : Controller
    {
        private readonly ExchangeService _service;

        public HomeController()
        {
            var repository = new SqlRepository();
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
                var context = GlobalHost.ConnectionManager.GetHubContext<ExchangeHub>();
                context.Clients.All.refresh();
            }
            return PartialView("CreateOrder", order);
        }

        [HttpGet]
        public ActionResult CreateOrder(OrderType orderType)
        {
            var order = new Order {OrderType = orderType};
            return PartialView("CreateOrder", order);
        }

        public ActionResult GetBuyOrders()
        {
            var list = _service.GetBuyOrders();
            return PartialView("ListOrders", list);
        }

        public ActionResult GetSellOrders()
        {
            var list = _service.GetSellOrders();
            return PartialView("ListOrders", list);
        }

        public ActionResult GetTradeHistory()
        {
            var list = _service.GetTradeHistory();
            return PartialView("TradeHistory", list);
        }

    }
}
