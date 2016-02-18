namespace MvcApp.Controllers
{
    using System;
    using System.Threading;
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
            try
            {
                //Thread.Sleep(1000);
                if (ModelState.IsValid)
                {
                    _service.ProcessOrder(order);
                    var context = GlobalHost.ConnectionManager.GetHubContext<ExchangeHub>();
                    context.Clients.All.refresh();
                    return PartialView("CreateOrder", order);
                }
            }
            catch (Exception ex)
            {
                // log ex.ToString()
                ModelState.AddModelError(string.Empty, "Sorry, we have some problems. Take a break and try again.");
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
            try
            {
                var list = _service.GetBuyOrders();
                return PartialView("ListOrders", list);
            }
            catch (Exception ex)
            {
                // log ex.ToString()
                ModelState.AddModelError(string.Empty, "Sorry, we have some problems. Take a break and try again."); 
            }
            return PartialView("ListOrders", null);
        }

        public ActionResult GetSellOrders()
        {
            try
            {
                var list = _service.GetSellOrders();
                return PartialView("ListOrders", list);
            }
            catch (Exception ex)
            {
                // log ex.ToString()
                ModelState.AddModelError(string.Empty, "Sorry, we have some problems. Take a break and try again.");
            }
            return PartialView("ListOrders", null);
        }

        public ActionResult GetTradeHistory()
        {
            try
            {
                var list = _service.GetTradeHistory();
                return PartialView("TradeHistory", list);
            }
            catch (Exception ex)
            {
                // log ex.ToString()
                ModelState.AddModelError(string.Empty, "Sorry, we have some problems. Take a break and try again.");
            }
            return PartialView("TradeHistory", null);
        }

    }
}
