namespace MvcApp.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Models.DataAccess;
    using Models.Domain;

    [TestClass]
    public class ExchangeServiceTest
    {
        private ExchangeService _service;

        [TestInitialize]
        public void Initialize()
        {
            var repository = new FakeRepository();
            _service = new ExchangeService(repository);
        }

        [TestMethod]
        public void FirstTradedMoreProfitableSellOrders()
        {
            // 2) сначала торгуются более выгодные ордеры
            // 3) цена определяется ранее открытым ордером

            var sell1 = new Order { OrderType = OrderType.Sell, TotalAmount = 5, Price = 10 };
            var sell2 = new Order { OrderType = OrderType.Sell, TotalAmount = 5, Price = 11 };
            var sell3 = new Order { OrderType = OrderType.Sell, TotalAmount = 5, Price = 9 };
            var buyOrder = new Order { OrderType = OrderType.Buy, TotalAmount = 5, Price = 10.5m };

            _service.ProcessOrder(sell1);
            _service.ProcessOrder(sell2);
            _service.ProcessOrder(sell3);
            _service.ProcessOrder(buyOrder);

            var realTrades = _service.GetTradeHistory();

            Assert.AreEqual(realTrades.Count, 1);
            Assert.AreEqual(realTrades[0].Price, 9);
            Assert.AreEqual(realTrades[0].Amount, 5);

            Assert.AreEqual(realTrades[0].BuyOrder.OrderType, OrderType.Buy);
            Assert.AreEqual(realTrades[0].BuyOrder.Price, 10.5m);
            Assert.AreEqual(realTrades[0].BuyOrder.TotalAmount, 5);

            Assert.AreEqual(realTrades[0].SellOrder.OrderType, OrderType.Sell);
            Assert.AreEqual(realTrades[0].SellOrder.Price, 9);
            Assert.AreEqual(realTrades[0].SellOrder.TotalAmount, 5);
        }

        [TestMethod]
        public void FirstTradedMoreProfitableBuyOrders()
        {
            // 2) сначала торгуются более выгодные ордеры
            // 3) цена определяется ранее открытым ордером

            var buy1 = new Order { OrderType = OrderType.Buy, TotalAmount = 5, Price = 10 };
            var buy2 = new Order { OrderType = OrderType.Buy, TotalAmount = 5, Price = 11 };
            var buy3 = new Order { OrderType = OrderType.Buy, TotalAmount = 5, Price = 12 };
            var sellOrder = new Order {OrderType = OrderType.Sell, TotalAmount = 5, Price = 10.5m};

            _service.ProcessOrder(buy1);
            _service.ProcessOrder(buy2);
            _service.ProcessOrder(buy3);
            _service.ProcessOrder(sellOrder);

            var realTrades = _service.GetTradeHistory();

            Assert.AreEqual(realTrades.Count, 1);
            Assert.AreEqual(realTrades[0].Price, 12);
            Assert.AreEqual(realTrades[0].Amount, 5);

            Assert.AreEqual(realTrades[0].BuyOrder.OrderType, OrderType.Buy);
            Assert.AreEqual(realTrades[0].BuyOrder.Price, 12);
            Assert.AreEqual(realTrades[0].BuyOrder.TotalAmount, 5);

            Assert.AreEqual(realTrades[0].SellOrder.OrderType, OrderType.Sell);
            Assert.AreEqual(realTrades[0].SellOrder.Price, 10.5m);
            Assert.AreEqual(realTrades[0].SellOrder.TotalAmount, 5);
        }

        [TestMethod]
        public void BuyPriceMustBeMoreOrEqualThanSellPrice()
        {
            // 1) цена покупки не ниже чем цена продажи

            var sell1 = new Order { OrderType = OrderType.Sell, TotalAmount = 5, Price = 10 };
            var sell2 = new Order { OrderType = OrderType.Sell, TotalAmount = 5, Price = 11 };
            var sell3 = new Order { OrderType = OrderType.Sell, TotalAmount = 5, Price = 9 };
            var buyOrder = new Order { OrderType = OrderType.Buy, TotalAmount = 5, Price = 5 };

            _service.ProcessOrder(sell1);
            _service.ProcessOrder(sell2);
            _service.ProcessOrder(sell3);
            _service.ProcessOrder(buyOrder);

            var realTrades = _service.GetTradeHistory();

            Assert.AreEqual(realTrades.Count, 0);
        }

        [TestMethod]
        public void SellPriceMustBeLessOrEqualThenBuyPrice()
        {
            // 1) цена продажи не выше чем цена покупки

            var buy1 = new Order { OrderType = OrderType.Buy, TotalAmount = 5, Price = 10 };
            var buy2 = new Order { OrderType = OrderType.Buy, TotalAmount = 5, Price = 11 };
            var buy3 = new Order { OrderType = OrderType.Buy, TotalAmount = 5, Price = 12 };
            var sellOrder = new Order { OrderType = OrderType.Sell, TotalAmount = 5, Price = 15};

            _service.ProcessOrder(buy1);
            _service.ProcessOrder(buy2);
            _service.ProcessOrder(buy3);
            _service.ProcessOrder(sellOrder);

            var realTrades = _service.GetTradeHistory();

            Assert.AreEqual(realTrades.Count, 0);
        }
    }
}
