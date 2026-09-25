using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03WeatherForecastWPF.Client.Models
{
    internal class OpenMeteoForecastResponse
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationtimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("current_units")]
        public CurrentUnits CurrentUnits { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }
    }
}
