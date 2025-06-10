using VendingMachine.Services;

namespace VendingMachine.IntegrationTests
{
    [TestFixture]
    public class VendingMachineIntegrationTests
    {
        private PaymentService _paymentService;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _paymentService = new PaymentService();
            _productService = new ProductService(_paymentService);
        }

        [Test]
        public void InsertCoins_AndBuyCola_WithExactAmount_ShouldReturnThankYou()
        {
            _paymentService.AcceptCoin("quarter");
            _paymentService.AcceptCoin("quarter");
            _paymentService.AcceptCoin("quarter");
            _paymentService.AcceptCoin("quarter");

            var result = _productService.SelectProduct("cola");

            Assert.AreEqual("THANK YOU", result);
            Assert.AreEqual("INSERT COIN", _paymentService.DisplayMessage());
        }

        [Test]
        public void InsertCoins_AndTryToBuyChips_WithInsufficientFunds_ShouldReturnPrice()
        {
            _paymentService.AcceptCoin("dime");
            _paymentService.AcceptCoin("nickel");

            var result = _productService.SelectProduct("chips");

            Assert.AreEqual("PRICE: $0.50", result);
            Assert.AreEqual("Current amount: $0.15", _paymentService.DisplayMessage());
        }

        [Test]
        public void TryToBuyProductWithoutInsertingCoin_ShouldReturnPrice()
        {
            var result = _productService.SelectProduct("candy");

            Assert.AreEqual("PRICE: $0.65", result);
            Assert.AreEqual("INSERT COIN", _paymentService.DisplayMessage());
        }

        [Test]
        public void InsertInvalidCoin_ShouldNotAffectBalance()
        {
            _paymentService.AcceptCoin("penny");

            Assert.AreEqual(0m, _paymentService.GetCurrentAmount());
            Assert.AreEqual("INSERT COIN", _paymentService.DisplayMessage());
        }

        [Test]
        public void InsertCoins_PurchaseCandy_DisplayAndAmountReset()
        {
            _paymentService.AcceptCoin("quarter");
            _paymentService.AcceptCoin("quarter");
            _paymentService.AcceptCoin("dime");
            _paymentService.AcceptCoin("nickel");

            var result = _productService.SelectProduct("candy");

            Assert.AreEqual("THANK YOU", result);
            Assert.AreEqual(0m, _paymentService.GetCurrentAmount());
            Assert.AreEqual("INSERT COIN", _paymentService.DisplayMessage());
        }

        [Test]
        public void SelectInvalidProduct_ShouldReturnInvalidMessage()
        {
            _paymentService.AcceptCoin("quarter");

            var result = _productService.SelectProduct("soda");

            Assert.AreEqual("INVALID PRODUCT", result);
            Assert.AreEqual("Current amount: $0.25", _paymentService.DisplayMessage());
        }
    }
}
