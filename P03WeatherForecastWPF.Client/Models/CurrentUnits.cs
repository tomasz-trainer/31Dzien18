using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03WeatherForecastWPF.Client.Models
{
    internal class CurrentUnits
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("temperature_2m")]
        public string Temperature2m { get; set; }
    }
}
