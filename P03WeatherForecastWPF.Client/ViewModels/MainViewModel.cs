using P03WeatherForecastWPF.Client.Commands;
using P03WeatherForecastWPF.Client.Models;
using P03WeatherForecastWPF.Client.Services;
using P04WeatherForecastConsole.Client;
using P06Shop.Shared.Models;
using P06Shop.Shared.Services.WeatherSeervice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace P03WeatherForecastWPF.Client.ViewModels
{
    internal class MainViewModel : BaseViewModel, IMainViewModel
    {
        private string _cityName = "Warszawa";
        private City[] _cities;
        private City _selectedCity;
        private Weather _weather;
        private IMeteoService _ims;


        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
            }
        }

        public City[] Cities
        {
            get { return _cities; }
            set { 

                _cities = value;
                OnPropertyChanged();
            }
        }

        public City SelectedCity
        {
            get { return _selectedCity; }
            set 
            { 
                _selectedCity = value;
                OnPropertyChanged();
                loadWeather();
            }
        }

        public Weather Weather
        {
            get { return _weather; }
            set { 
                _weather = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadCitiesCommand { get; }


        public MainViewModel(IMeteoService meteoService)
        {
            LoadCitiesCommand = new RelayCommand(x => loadCities());
            _ims = meteoService;
        }

        private async void loadCities()
        {
            //Cities = new City[]
            //{
            //    new City { Name = "Warszawa", Country = "Polska" },
            //    new City { Name = "Kraków", Country = "Polska" },
            //    new City { Name = "Wrocław", Country = "Polska" },
            //    new City { Name = "Gdańsk", Country = "Polska" },
            //    new City { Name = "Poznań", Country = "Polska" }
            //};
           // OnPropertyChanged("Cities");

           
            Cities = await _ims.GetLocationsAsync(_cityName);
        }

        private async void loadWeather()
        {
            if (SelectedCity != null)
            {
                //Weather = new Weather
                //{
                //    Time = DateTime.Now.ToString("HH:mm:ss"),
                //    Temperature2m = 27
                //};
                Weather = await _ims.GetCurrentConditionsAsync(SelectedCity.Latitude, SelectedCity.Longitude);
            }
        }
    }
}