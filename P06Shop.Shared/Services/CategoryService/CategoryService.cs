using P06Shop.Shared;
using P06Shop.Shared.Services.CategoryService;
using System.Net.Http.Json;

namespace P03WeatherForecastWPF.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("api/category");
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>();
            return result ?? new ServiceResponse<List<Category>> { Success = false, Message = "Empty response from API." };
        }

        public async Task<ServiceResponse<Category>> CreateCategoryAsync(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync("api/category", category);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Category>>();
            return result ?? new ServiceResponse<Category> { Success = false, Message = "Empty response from API." };
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/category/{id}");
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            return result ?? new ServiceResponse<bool> { Success = false, Message = "Empty response from API." };
        }
    }
}
