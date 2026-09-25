using Microsoft.AspNetCore.Mvc;
using P05Shop.API.Services;
using P06Shop.Shared;
using P06Shop.Shared.Models;
using P06Shop.Shared.Services.ProductService;
using P06Shop.Shared.Services.WeatherSeervice;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherMeteoController : Controller
    {
        private readonly IMeteoService _meteoService;
        public WeatherMeteoController(IMeteoService meteoService)
        {
            _meteoService = meteoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetCities([FromQuery] string locationName)
        { 
            try
            {
                var result = await _meteoService.GetLocationsAsync(locationName);

                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpGet("getTemperature")]
        public async Task<ActionResult<Weather>> GetWeather([FromQuery] double latitude, [FromQuery] double longitude)
        {
            try
            {
                var result = await _meteoService.GetCurrentConditionsAsync(latitude, longitude);

                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
