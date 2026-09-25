using P03WeatherForecastWPF.Client.Models;
using P06Shop.Shared.Models;
using P06Shop.Shared.Services.WeatherSeervice;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03WeatherForecastWPF.Client.Services
{
    internal class FakeMeteoService : IMeteoService
    {
        public Task<Weather> GetCurrentConditionsAsync(double latitude, double longitude)
        {
            return Task.FromResult(new Weather
            {
                Temperature2m = 20.5,
                Time = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")
            });
        }

        public Task<City[]> GetLocationsAsync(string locationName)
        {
            return Task.FromResult(new City[]
            {
                new City { Name = "Warszawa", Country = "Polska", Latitude = 52.2297, Longitude = 21.0122 },
                new City { Name = "Kraków", Country = "Polska", Latitude = 50.0647, Longitude = 19.9450 },
                new City { Name = "Gdańsk", Country = "Polska", Latitude = 54.3520, Longitude = 18.6466 }
            });
        }
    }
}
