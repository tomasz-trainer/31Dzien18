using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace P03WeatherForecastWPF.Client.Converters
{
    internal class TemperatureToDisplayConverter : IValueConverter
    {
        private const string _tempCode = "°C";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
                return $"{value}{_tempCode}";

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string temperatureString = value as string;
            if(temperatureString != null && temperatureString.EndsWith(_tempCode))
            {
                string numberPart = temperatureString.Substring(0, temperatureString.Length - _tempCode.Length);
                if (double.TryParse(numberPart, out double result))
                {
                    return result;
                }
            }
            return value;
        }
    }
}
