using P06Shop.Shared;

namespace P12MAUI.Client.ViewModels
{
    public class ProductListItem
    {
        public ProductListItem(Product product, string categoryName)
        {
            Product = product;
            CategoryName = categoryName;
        }

        public Product Product { get; }

        public string CategoryName { get; }
    }
}
