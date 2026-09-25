using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03WeatherForecastWPF.Client.Models
{
    internal class Current
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("interval")]
        public int Interval { get; set; }

        [JsonProperty("temperature_2m")]
        public double Temperature2m { get; set; }
    }
}
