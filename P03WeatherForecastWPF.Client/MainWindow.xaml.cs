using P03WeatherForecastWPF.Client.Models;
using P03WeatherForecastWPF.Client.ViewModels;
using P04WeatherForecastConsole.Client;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P03WeatherForecastWPF.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow(IMainViewModel mainViewModel)
        {
            InitializeComponent();
           
         //   MainViewModel mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }

      

        
    }
}