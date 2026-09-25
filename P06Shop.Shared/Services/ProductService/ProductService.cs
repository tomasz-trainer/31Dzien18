using Newtonsoft.Json;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace P03WeatherForecastWPF.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<Product>> CreateProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/product", product);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Product>>();
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/product/{id}");
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            return result;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
             var response = await _httpClient.GetAsync($"api/product/{id}");
             var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Product>>();
             return result;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("api/product");
           // var json = await response.Content.ReadAsStringAsync();
           // var result = JsonConvert.DeserializeObject<ServiceResponse<List<Product>>>(json);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<Product>>>();
            return result;
        }

        public async Task<ServiceResponse<PagedResult<Product>>> SearchProductsAsync(string? text, int page, int pageSize)
        {
            var url = $"api/product/search?text={text}&page={page}&pageSize={pageSize}";
            var response = await _httpClient.GetAsync(url);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<PagedResult<Product>>>();
            return result;
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync("api/product", product);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Product>>();
            return result;
        }
    }
}
