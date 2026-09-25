using System;
using System.Collections.Generic;
using System.Text;

namespace P06Shop.Shared.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();

        Task<ServiceResponse<Product>> GetProductAsync(int id);

        Task<ServiceResponse<Product>> CreateProductAsync(Product product);

        Task<ServiceResponse<bool>> DeleteProductAsync(int id);

        Task<ServiceResponse<Product>> UpdateProductAsync(Product product);

        Task<ServiceResponse<PagedResult<Product>>> SearchProductsAsync(string? text, int page, int pageSize);

    }
}
