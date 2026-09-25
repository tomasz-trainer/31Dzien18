namespace P06Shop.Shared.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();

        Task<ServiceResponse<Category>> CreateCategoryAsync(Category category);

        Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);
    }
}
