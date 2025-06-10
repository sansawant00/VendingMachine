namespace VendingMachine.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        private static readonly List<Product> _availableProducts = new()
        {
             new Product { Name = "cola", Price = 1.00m },
             new Product { Name = "chips", Price = 0.50m },
             new Product { Name = "candy", Price = 0.65m }
        };

        public static IReadOnlyList<Product> AvailableProducts => _availableProducts;

        public static Product GetProduct(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            return _availableProducts
                .FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}