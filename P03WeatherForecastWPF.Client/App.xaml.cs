using Microsoft.Extensions.DependencyInjection;
using P03WeatherForecastWPF.Client.MessageBox;
using P03WeatherForecastWPF.Client.Services;
using P03WeatherForecastWPF.Client.ViewModels;
using P04WeatherForecastConsole.Client;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using P06Shop.Shared.Services.WeatherSeervice;
using System.Configuration;
using System.Data;
using System.Windows;

namespace P03WeatherForecastWPF.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IServiceProvider _serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

        }

        private void ConfigureServices(IServiceCollection services)
        {
            ConfigureAppServices(services);
            ConfigureViewModels(services);
            ConfigureViews(services);
            ConfigureHttpClients(services);
        }

        private void ConfigureHttpClients(IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri("http://20.86.59.56"); // ustawienie adresu bazowego dla klienta HTTP
                });

            services.AddHttpClient<IMeteoService, LocalMeteoService>()
           .ConfigureHttpClient(client =>
           {
               client.BaseAddress = new Uri("http://20.86.59.56"); // ustawienie adresu bazowego dla klienta HTTP
           });
        }

        private void ConfigureViews(IServiceCollection services)
        {
            services.AddTransient<MainWindow>(); // tworzy instancję MainWindow przy każdym żądaniu
            services.AddTransient<SecondWindow>();
            services.AddTransient<ShopProductsView>();
            services.AddTransient<ProductDetailsView>();
        }

        private void ConfigureViewModels(IServiceCollection services)
        {
            services.AddSingleton<IMainViewModel, MainViewModelV2>(); // rejestracja MainViewModel jako singleton
            services.AddSingleton<SecondWindowViewModel>();
            services.AddSingleton<ProductsViewModel>(); // rejestracja ProductsViewModel jako singleton

        }

        private void ConfigureAppServices(IServiceCollection services)
        {
           // services.AddSingleton<IMeteoService, OpenMeteoService>(); // rejestracja serwisu jako singleton
            services.AddSingleton<IMeteoService, LocalMeteoService>(); // rejestracja serwisu jako singleton
            services.AddSingleton<IProductService, ProductService>(); // rejestracja serwisu jako singleton
            services.AddSingleton<IMeesageDialogService, WpfMessageDialogService>(); // rejestracja serwisu jako singleton

        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

    }
}
