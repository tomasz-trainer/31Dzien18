using Microsoft.EntityFrameworkCore;
using P05Shop.API.Models;
using P06Shop.Shared;
using P06Shop.Shared.Services.CategoryService;

namespace P05Shop.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var result = new ServiceResponse<List<Category>>();
            try
            {
                result.Data = await _dataContext.Categories.OrderBy(c => c.Name).ToListAsync();
                result.Success = true;
                result.Message = "Categories retrieved successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error occurred while retrieving categories: {ex.Message}";
            }
            return result;
        }

        public async Task<ServiceResponse<Category>> CreateCategoryAsync(Category category)
        {
            var result = new ServiceResponse<Category>();
            var name = category.Name?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                result.Success = false;
                result.Message = "Category name is required.";
                return result;
            }

            if (await _dataContext.Categories.AnyAsync(c => c.Name == name))
            {
                result.Success = false;
                result.Message = $"Category '{name}' already exists.";
                return result;
            }

            try
            {
                var newCategory = new Category { Name = name };
                await _dataContext.Categories.AddAsync(newCategory);
                await _dataContext.SaveChangesAsync();
                result.Data = newCategory;
                result.Success = true;
                result.Message = "Category created successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error occurred while creating the category: {ex.Message}";
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            var result = new ServiceResponse<bool>();
            var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                result.Success = false;
                result.Message = "Category not found.";
                return result;
            }

            try
            {
                _dataContext.Categories.Remove(category);
                await _dataContext.SaveChangesAsync();
                result.Data = true;
                result.Success = true;
                result.Message = "Category deleted successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error occurred while deleting the category: {ex.Message}";
            }
            return result;
        }
    }
}
