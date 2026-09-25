using Newtonsoft.Json;
using P06Shop.Shared;
using P06Shop.Shared.Models;
using P06Shop.Shared.Services.ProductService;
using P06Shop.Shared.Services.WeatherSeervice;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace P03WeatherForecastWPF.Client.Services
{
    internal class LocalMeteoService : IMeteoService
    {
        private readonly HttpClient _httpClient;

        public LocalMeteoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Weather> GetCurrentConditionsAsync(double latitude, double longitude)
        {
            var response = await _httpClient.GetAsync($"api/WeatherMeteo/getTemperature?latitude={latitude}&longitude={longitude}");
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Weather>(json);
            return result;
        }

        public async Task<City[]> GetLocationsAsync(string locationName)
        {
            var response = await _httpClient.GetAsync($"api/WeatherMeteo?locationName={locationName}");
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<City[]> (json);
            return result;
        }



    }
}
