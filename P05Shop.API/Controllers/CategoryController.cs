using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared;
using P06Shop.Shared.Services.CategoryService;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
        {
            var result = await _categoryService.GetCategoriesAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return StatusCode(500, result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Category>>> AddCategory([FromBody] Category category)
        {
            var result = await _categoryService.CreateCategoryAsync(category);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCategory([FromRoute] int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
