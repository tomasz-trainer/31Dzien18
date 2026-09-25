using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using P05Shop.API.Models;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using P07Shop.DataSeeder;

namespace P05Shop.API.Services
{
    public class ProductService : IProductService
    {

        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Product>> CreateProductAsync(Product product)
        {
             var result = new ServiceResponse<Product>();

            try
            {
                await _dataContext.Products.AddAsync(product);
                await _dataContext.SaveChangesAsync();

                result.Data = product;
                result.Success = true;
                result.Message = "Product created successfully.";
            }
            catch (Exception ex)
            {
                 result.Success = false;
                 result.Message = $"An error occurred while creating the product: {ex.Message}";
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync (int id)
        {
            var result = new ServiceResponse<bool>();

            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);

           

            if (product != null)
            {
                Console.WriteLine(product.Title + " " + product.Description);


                if (product.Price > 1000)
                {
                    result.Message = "Product cannot be deleted because its price is greater than 1000.";
                    result.Success = false;
                    return result;
                }


                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();


                result.Data = true;
                result.Success = true;
                result.Message = "Product deleted successfully.";
            }
            else
            {
                result.Message = "Product not found.";
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteProductOneQueryAsync(int id)
        {
            var result = new ServiceResponse<bool>();


            try
            {
                var product = new Product { Id = id };
                _dataContext.Products.Attach(product);
                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();

                result.Data = true;
                result.Success = true;
                result.Message = "Product deleted successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.Message = "Product not found.";
                result.Success = false;
            }
            return result;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
            var result = new ServiceResponse<Product>();

            try
            {
                result.Data = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                result.Success = true;
                result.Message = "Product retrieved successfully.";
            }
            catch (Exception ex )
            {
                 result.Message = $"An error occurred while retrieving the product: {ex.Message}";
                 result.Success = false;
            }
            return result;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var result = new ServiceResponse<List<Product>>();

            try
            {
                result.Data =  await _dataContext.Products.ToListAsync();
                result.Success = true;
                result.Message = "Products retrieved successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error occurred while retrieving products: {ex.Message}";

            }
            return result;
        }

        public async Task<ServiceResponse<PagedResult<Product>>> SearchProductsAsync(string? text, int page, int pageSize)
        {
            var result = new ServiceResponse<PagedResult<Product>>();
            try
            {
                IQueryable<Product> query = _dataContext.Products;

                if (!string.IsNullOrEmpty(text))
                {
                    query = query.Where(p => p.Title.Contains(text) || p.Description.Contains(text));
                }

                var totalCount = await query.CountAsync();

                var data= await query.OrderBy(x=>x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                result.Data = new PagedResult<Product>()
                {
                    Items = data,
                    TotalCount = totalCount
                };


                result.Success = true;
                result.Message = "Products retrieved successfully.";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error occurred while searching for products: {ex.Message}";
            }

            return result;
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(Product product)
        {
            var result = new ServiceResponse<Product>();

            try
            {
                _dataContext.Products.Update(product);

                // lub 
                //var editedProduct = new Product { Id = product.Id };
                //_dataContext.Products.Attach(product);

                //editedProduct.Title = product.Title;
                //editedProduct.Description = product.Description;
                //editedProduct.Barcode = product.Barcode;
                //editedProduct.Price = product.Price;
                //editedProduct.ReleaseDate = product.ReleaseDate;

                await _dataContext.SaveChangesAsync();

                result.Data = product;
                result.Success = true;
                result.Message = "Product updated successfully.";
            }
            catch (Exception ex)
            {
                 result.Success = false;
                result.Message = $"An error occurred while updating the product: {ex.Message}";
            }
            return result;

        }
    }
}
