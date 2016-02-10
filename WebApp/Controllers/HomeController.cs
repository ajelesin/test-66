using System.Web.Mvc;
using WebApp.Models.DataAccess;
using WebApp.Models.Domain;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExchangeService _service;

        public HomeController()
        {
            var repository = new SqlRepository();
            _service = new ExchangeService(repository);
        }

        public ActionResult BuyOrders()
        {
            var result = _service.GetBuyOrders();
            return PartialView("Orders", result);
        }

        public ActionResult SellOrders()
        {
            var result = _service.GetSellOrders();
            return PartialView("Orders", result);
        }

        public ActionResult History()
        {
            var result = _service.GetTradeHistory();
            return PartialView(result);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(BaseOrder order)
        {
            var result = _service.ProcessOrder(order);
            return View(result);
        }

    }
}
