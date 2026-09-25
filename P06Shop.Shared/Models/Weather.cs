using System;
using System.Collections.Generic;
using System.Text;

namespace P06Shop.Shared.Models
{
    public class Weather
    {
        public string Time { get; set; }
        public double Temperature2m { get; set; }

        public bool IsVisible =>  Convert.ToInt32(Temperature2m) % 2 == 0; // przykładowa logika widoczności, np. tylko parzyste temperatury są widoczne
    }
}
