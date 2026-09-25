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

        [ObservableProperty]
        private ObservableCollection<Product> _products;

        [ObservableProperty]
        private Product _selectedProduct;
                         
        [ObservableProperty]
        private string _errorMessage;

        public ProductsViewModel(IProductService productService, ProductDetailsView productDetailsView, IMeesageDialogService meesageDialogService)
        {
            _productService = productService;
            _productDetailsView = productDetailsView;
            _messageDialogService = meesageDialogService;

            LoadProductsAsync();
        }

        public async Task LoadProductsAsync()
        {
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

       


    }
}
