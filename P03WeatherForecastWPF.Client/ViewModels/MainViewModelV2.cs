using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using P03WeatherForecastWPF.Client.Commands;
using P03WeatherForecastWPF.Client.Models;
using P03WeatherForecastWPF.Client.Services;
using P04WeatherForecastConsole.Client;
using P06Shop.Shared.Models;
using P06Shop.Shared.Services.WeatherSeervice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace P03WeatherForecastWPF.Client.ViewModels
{

    // zastosowanie biblioteki CommunityToolkit
    // Observale object dodaje mechanizm powiadamiania UI o zmianach właściwości, więc nie trzeba implementować INotifyPropertyChanged ręcznie
    internal partial class MainViewModelV2 : ObservableObject, IMainViewModel
    {
        private string _cityName = "Warszawa";
        //private City[] _cities;
        private City _selectedCity;
        //private Weather _weather;
        private IMeteoService _ims;

        [ObservableProperty]
        private Weather weather;

        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        //public City[] Cities
        //{
        //    get { return _cities; }
        //    set { 

        //        _cities = value;
        //        OnPropertyChanged();
        //    }
        //}

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

        //public Weather Weather
        //{
        //    get { return _weather; }
        //    set { 
        //        _weather = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public ICommand LoadCitiesCommand { get; }


         private readonly IServiceProvider _serviceProvider;

        public MainViewModelV2(IMeteoService meteoService, IServiceProvider serviceProvider)
        {
            //LoadCitiesCommand = new RelayCommand(x => loadCities());
            _ims = meteoService;
            Cities = new ObservableCollection<City>();
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        public async void LoadCities()
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

           
            var cities = await _ims.GetLocationsAsync(_cityName);

            Cities.Clear();
            foreach (var city in cities)
                Cities.Add(city);
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

        [RelayCommand]
        public async Task OpenWindow()
        {
            var secondWindow = _serviceProvider.GetService<ShopProductsView>();
            var secondWindowViewModel = _serviceProvider.GetService<ProductsViewModel>();
         
            secondWindow.DataContext = secondWindowViewModel;
            await secondWindowViewModel.LoadProductsAsync();

            secondWindow.Show();
        }
    }
}