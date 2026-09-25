using P06Shop.Shared.Models;
using P06Shop.Shared.Services.WeatherSeervice;

namespace P05Shop.API.Services
{
    public class MeteoService : IMeteoService
    {
        public Task<Weather> GetCurrentConditionsAsync(double latitude, double longitude)
        {
            return Task.FromResult(new Weather
            {
              Temperature2m = 22.5,
              Time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            });
        }

        public Task<City[]> GetLocationsAsync(string locationName)
        {
            return Task.FromResult(new City[]
            {
                new City { Name = "New York", Country = "USA", Latitude = 40.7128, Longitude = -74.0060 },
                new City { Name = "Los Angeles", Country = "USA", Latitude = 34.0522, Longitude = -118.2437 },
                new City { Name = "London", Country = "UK", Latitude = 51.5074, Longitude = -0.1278 },
                new City { Name = "Paris", Country = "France", Latitude = 48.8566, Longitude = 2.3522 },
                new City { Name = "Tokyo", Country = "Japan", Latitude = 35.6895, Longitude = 139.6917 }
            });
        }
    }
}
