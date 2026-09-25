using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03WeatherForecastWPF.Client.ViewModels
{
    public partial class SecondWindowViewModel : ObservableObject, IMainViewModel
    {
        [ObservableProperty]
        private string title;

      
    }
}
