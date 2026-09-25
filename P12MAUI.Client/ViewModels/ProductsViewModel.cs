using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using P12MAUI.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace P12MAUI.Client.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {

        private readonly IProductService _productService;
        private readonly ProductDetailsView _productDetailsView;
        private readonly IMeesageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;
        private readonly IGeolocation _geolocation;
        private readonly IMap _map;

        [ObservableProperty]
        private ObservableCollection<Product> _products;

        [ObservableProperty]
        private Product _selectedProduct;
                         
        [ObservableProperty]
        private string _errorMessage;

        public ProductsViewModel(IProductService productService,
            ProductDetailsView productDetailsView, 
            IMeesageDialogService meesageDialogService,
            IConnectivity connectivity,
            IGeolocation geolocation,
            IMap map
            )
        {
            _productService = productService;
            _productDetailsView = productDetailsView;
            _messageDialogService = meesageDialogService;
            _connectivity = connectivity;
            _geolocation = geolocation;
            LoadProductsAsync();
        }

        public async Task LoadProductsAsync()
        {
            if(_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                ErrorMessage = "No internet connection. Please check your network settings.";
                _messageDialogService.ShowMessage(ErrorMessage);
                Products = new ObservableCollection<Product>();
                return;
            }

            var response = await _productService.GetProductsAsync();
            if (response.Success && response.Data != null)
            {
                
                Products = new ObservableCollection<Product>(response.Data);
            }
            else
            {
                // Handle error (e.g., log it, show a message to the user, etc.)
            //    Products = new ObservableCollection<Product>();
            }
        }

      
          

        [RelayCommand]
        public async Task NewProductWindow()
        {
            _selectedProduct = new Product(); // Create a new product instance
            _selectedProduct.ReleaseDate = DateTime.Now; // Set the default release date to now

            await Shell.Current.GoToAsync(nameof(ProductDetailsView), new Dictionary<string, object>
                 {
                     { nameof(Product), SelectedProduct},
                     { nameof(ProductsViewModel), this }
                 });
        }

        

        [RelayCommand]
        public async Task ShowProductDetails(Product product)
        {
            if (product != null)
            {
                await Shell.Current.GoToAsync(nameof(ProductDetailsView), new Dictionary<string, object>
                 {
                     { nameof(Product), product},
                     { nameof(ProductsViewModel), this }
                 });
            }
        }

        [RelayCommand]
        public async Task ShowMyLocationAsync()
        {
            try
            {
                var location = await _geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await _geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (location != null)
                {
                    var uri = new Uri($"https://www.google.com/maps?q={location.Latitude},{location.Longitude}");
                    await Launcher.OpenAsync(uri);


                    //var options = new MapLaunchOptions { Name = "My Location" };
                    //await _map.OpenAsync(location, options);
                }
                else
                {
                    ErrorMessage = "Unable to get location.";
                    _messageDialogService.ShowMessage(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error getting location: {ex.Message}";
                _messageDialogService.ShowMessage(ErrorMessage);
            }
        }


    }
}
