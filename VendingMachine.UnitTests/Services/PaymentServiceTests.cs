using VendingMachine.Services;

namespace VendingMachine.UnitTests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private PaymentService _paymentService;
        private static readonly string[] InvalidCoins = { "penny", "invalid", "" };

        [SetUp]
        public void Setup()
        {
            _paymentService = new PaymentService();
        }

        [TestCase("nickel", 0.05)]
        [TestCase("dime", 0.10)]
        [TestCase("quarter", 0.25)]
        public void AcceptCoin_ValidCoin_IncreasesAmount(string coin, double expectedValue)
        {
            _paymentService.AcceptCoin(coin);
            Assert.AreEqual((decimal)expectedValue, _paymentService.GetCurrentAmount());
        }


        [TestCaseSource(nameof(InvalidCoins))]
        public void AcceptCoin_InvalidCoin_DoesNotIncreaseAmount(string coin)
        {
            _paymentService.AcceptCoin(coin);
            Assert.AreEqual(0m, _paymentService.GetCurrentAmount());
        }


        [Test]
        public void DisplayMessage_NoCoins_ReturnsInsertCoin()
        {
            var message = _paymentService.DisplayMessage();
            Assert.AreEqual("INSERT COIN", message);
        }

        [Test]
        public void DisplayMessage_WithCoins_ReturnsCurrentAmount()
        {
            _paymentService.AcceptCoin("quarter");
            var message = _paymentService.DisplayMessage();
            Assert.AreEqual("Current amount: $0.25", message);
        }

        [Test]
        public void Reset_ResetsInsertedAmountToZero()
        {
            _paymentService.AcceptCoin("dime");
            _paymentService.Reset();
            Assert.AreEqual(0m, _paymentService.GetCurrentAmount());
            Assert.AreEqual("INSERT COIN", _paymentService.DisplayMessage());
        }
    }
}
