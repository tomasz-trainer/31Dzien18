using P06Shop.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03WeatherForecastWPF.Client.MessageBox
{
    internal class WpfMessageDialogService : IMeesageDialogService
    {
        public void ShowMessage(string message)
        {
            System.Windows.MessageBox.Show(message, "Message", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }
    }
}
