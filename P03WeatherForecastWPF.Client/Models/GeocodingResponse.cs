using Newtonsoft.Json;
using P06Shop.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03WeatherForecastWPF.Client.Models
{
    internal class GeocodingResponse
    {
        [JsonProperty("results")]
        public List<City> Results { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationtimeMs { get; set; }
    }
}
