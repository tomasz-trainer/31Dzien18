using Microsoft.AspNetCore.Mvc;
using P05Shop.API.Services;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            //ProductService productService = new ProductService();

            Task.Delay(1000).Wait(); // symulacja opóźnienia 1 sekundy

            var result = await _productService.GetProductsAsync();

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //https://localhost:5001/api/product/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct([FromRoute] int id)
        {

            var result = await _productService.DeleteProductAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct([FromBody] Product product)
        {
            var result = await _productService.UpdateProductAsync(product);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductById([FromRoute] int id)
        {
            var result = await _productService.GetProductAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Product>>> AddProduct([FromBody] Product product)
        {
            var result = await _productService.CreateProductAsync(product);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        //https://localhost:5001/api/product/delete?id=1
        // przyklad endpointu niezgodne z REST
        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProductOneQuery([FromQuery] int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        //https://localhost:5001/api/product/search?text=product&page=1&pageSize=10
        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<PagedResult<Product>>>> SearchProducts([FromQuery] string? text, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _productService.SearchProductsAsync(text, page, pageSize);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}