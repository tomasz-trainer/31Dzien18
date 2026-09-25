using Microsoft.Extensions.Logging;
using P03WeatherForecastWPF.Client.Services;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using P12MAUI.Client.MessageBox;
using P12MAUI.Client.ViewModels;

namespace P12MAUI.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            ConfigureServices(builder.Services);
            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            ConfigureAppServices(services);
            ConfigureViewModels(services);
            ConfigureViews(services);
            ConfigureHttpClients(services);
        }
        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddTransient<MainPage>(); // tworzy instancję MainWindow przy każdym żądaniu
            services.AddTransient<ProductDetailsView>();
 
        }

        private static void ConfigureViewModels(IServiceCollection services)
        {
            services.AddSingleton<ProductsViewModel>(); // rejestracja MainViewModel jako singleton
            services.AddSingleton<ProductDetailsViewModel>(); // rejestracja ProductDetailsViewModel jako singleton
        }

        private static void ConfigureAppServices(IServiceCollection services)
        {
            services.AddSingleton<IProductService, ProductService>(); // rejestracja serwisu jako singleton
            services.AddSingleton<IMeesageDialogService, MauiMessageDialogService>(); // rejestracja serwisu jako singleton
            services.AddSingleton<IConnectivity>(Connectivity.Current);
            services.AddSingleton<IGeolocation>(Geolocation.Default);
            services.AddSingleton<IMap>(Map.Default);
        }

        private static void ConfigureHttpClients(IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7127"); // ustawienie adresu bazowego dla klienta HTTP
                });
        }
    }
}
