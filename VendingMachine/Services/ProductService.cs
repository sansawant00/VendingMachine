using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine.Services
{
    public class ProductService : IProductService
    {
        private readonly IPaymentService _paymentService;

        public ProductService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public string SelectProduct(string productName)
        {
            var product = Product.GetProduct(productName);
            if (product == null)
                return "INVALID PRODUCT";

            var amountInserted = _paymentService.GetCurrentAmount();
            if (amountInserted >= product.Price)
            {
                _paymentService.Reset();
                return "THANK YOU";
            }
            else
            {
                return $"PRICE: ${product.Price:F2}";
            }
        }
    }
}
