using Moq;
using VendingMachine.Interfaces;
using VendingMachine.Services;

namespace VendingMachine.UnitTests.Services
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IPaymentService> _paymentServiceMock;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _paymentServiceMock = new Mock<IPaymentService>();
            _productService = new ProductService(_paymentServiceMock.Object);
        }

        [TestCase("cola", 1.00)]
        [TestCase("chips", 0.50)]
        [TestCase("candy", 0.65)]
        public void SelectProduct_SufficientAmount_ReturnsThankYou(string product, double insertedAmount)
        {
            _paymentServiceMock.Setup(p => p.GetCurrentAmount()).Returns((decimal)insertedAmount);

            var result = _productService.SelectProduct(product);

            Assert.AreEqual("THANK YOU", result);
            _paymentServiceMock.Verify(p => p.Reset(), Times.Once);
        }

        [TestCase("cola", 0.75, 1.00)]
        [TestCase("chips", 0.20, 0.50)]
        [TestCase("candy", 0.50, 0.65)]
        public void SelectProduct_InsufficientAmount_ReturnsPriceMessage(string product, double insertedAmount, double expectedPrice)
        {
            _paymentServiceMock.Setup(p => p.GetCurrentAmount()).Returns((decimal)insertedAmount);

            var result = _productService.SelectProduct(product);

            Assert.AreEqual($"PRICE: ${expectedPrice:F2}", result);
            _paymentServiceMock.Verify(p => p.Reset(), Times.Never);
        }

        [TestCase("soda")]
        [TestCase("")]
        public void SelectProduct_InvalidProduct_ReturnsInvalidProductMessage(string product)
        {
            var result = _productService.SelectProduct(product);

            Assert.AreEqual("INVALID PRODUCT", result);
            _paymentServiceMock.Verify(p => p.GetCurrentAmount(), Times.Never);
            _paymentServiceMock.Verify(p => p.Reset(), Times.Never);
        }
    }
}