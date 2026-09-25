using Bogus;
using P06Shop.Shared;

namespace P07Shop.DataSeeder
{
    public class ProductDataSeeder
    {
        public static List<Product> GenerateProductData()
        {
            var prodcutFaker = new Faker<Product>()
                .UseSeed(1234) // Optional: Use a seed for reproducibility
                .RuleFor(p => p.Id, f => f.IndexFaker + 1)
                .RuleFor(p => p.Title, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Barcode, f => f.Commerce.Ean13().Substring(12))
                .RuleFor(p => p.Price, f => f.Random.Double(1, 1000))
                .RuleFor(p => p.ReleaseDate, f => new DateTime(2023,1,1));

            var products = prodcutFaker.Generate(100);

            return products;
        }
    }
}
