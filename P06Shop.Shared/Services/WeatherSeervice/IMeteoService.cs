
using P06Shop.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P06Shop.Shared.Services.WeatherSeervice
{
    public interface IMeteoService
    {
        Task<City[]> GetLocationsAsync(string locationName);

        Task<Weather> GetCurrentConditionsAsync(double latitude, double longitude);
    }
}
