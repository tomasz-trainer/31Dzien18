using Microsoft.AspNetCore.Mvc;
using P05Shop.API.DTO;

namespace P05Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        //https://localhost:7127/api/WeatherForecast

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //https://localhost:7127/api/WeatherForecast/onlyTwo
        [HttpGet("onlyTwo")]
        public IEnumerable<WeatherForecast> GetOnlyTwoWeatherForeceast()
        {
            return new WeatherForecast[2]
            {
                new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    TemperatureC = 25,
                    Summary = "Sunny"
                },
                new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    TemperatureC = 22,
                    Summary = "Cloudy"
                }

            };
        }

        //https://localhost:7127/api/WeatherForecast/search?number=3
        [HttpGet("search")]
        public IEnumerable<WeatherForecast> GetWeatherForecasts([FromQuery] int number)
        {
            return Enumerable.Range(1, number).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        //https://localhost:7127/api/WeatherForecast/test/hello?number=3
        [HttpGet("test/{routeParam}")]
        public string GetValueFromPatch([FromQuery] int number, [FromRoute] string routeParam)
        {
            return $"Route parameter: {routeParam}, Query parameter: {number}";
        }

        //https://localhost:7127/api/WeatherForecast/filter?cityName=New%20York&country=USA
        [HttpGet("filter")]
        public string GetValueFromPatch([FromQuery] string cityName, [FromQuery] string country)
        {
            return $"City: {cityName}, Country: {country}";
        }



        //https://localhost:7127/api/WeatherForecast/filter?name=New%20York&country=USA
        [HttpGet("complexfilter")]
        public string GetValueFromPatch([FromQuery] CityDto cityDto)
        {
            return $"City: {cityDto.Name}, Country: {cityDto.Country}";
        }


        //https://localhost:7127/api/WeatherForecast
        [HttpPost]
        public string AddNewCity([FromBody] CityDto cityDto)
        {
            return $"City: {cityDto.Name}, Country: {cityDto.Country}";
        }

        // GET  - pobieranie danych
        // POST - dodawanie danych
        // PUT - aktualizacja danych
        // Delete - usuwanie danych

        //https://localhost:7127/api/WeatherForecast/newCity
        //https://localhost:7127/api/WeatherForecast/addCity
        [HttpPost("newCity")]
        [HttpPost("addCity")]
        public string MultipleRoutes([FromBody] CityDto cityDto)
        {
            return $"City: {cityDto.Name}, Country: {cityDto.Country}";
        }

        // zwracanie statusu kodów HTTP

        //https://localhost:7127/api/WeatherForecast/addNewCity
        [HttpPost("addNewCity")]
        public IActionResult AddCityWithStatusCode([FromBody] CityDto cityDto)
        {
            // return StatusCode(201, $"City: {cityDto.Name}, Country: {cityDto.Country} added successfully.");

            // return Ok($"City: {cityDto.Name}, Country: {cityDto.Country} added successfully.");


            if (cityDto.Name == null)
            {
                return BadRequest("name is required");
            }


            try
            {
                // proboa dodania do bazy danych

            }
            catch (Exception ex)
            {
                //ex.Message
                return StatusCode(500, ex.Message);
            }


            int id = 4;

            return Created($"https://localhost:7127/api/WeatherForecast/GetCity/{id}", $"City: {cityDto.Name}, Country: {cityDto.Country} added successfully.");
        }

        [HttpGet("GetCity/{id}")]
        public IActionResult GetCity([FromRoute] int id)
        {
            // pobranie miasta z bazy danych na podstawie id
            CityDto city = new CityDto
            {
                Name = "New York",
                Country = "USA"
            };
            return Ok(city);
        }

        //https://localhost:7127/api/WeatherForecast/MyDynamincMathodName
        [HttpGet("[action]")]
        public IActionResult MyDynamincMathodName()
        {
            return Ok("This is a dynamic method name based on the action name.");
        }
    }
}
