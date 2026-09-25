using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace P03WeatherForecastWPF.Client.ViewModels
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
                 Products = new ObservableCollection<Product>();
            }
        }

      

        private async Task createProductAsync()
        {
            var result = await _productService.CreateProductAsync(_selectedProduct);
            if (result.Success)
            {
                await LoadProductsAsync();
            }
            else
            {
                _errorMessage = "Error creating product: " + result.Message;
                _messageDialogService.ShowMessage("Error creating product: " + result.Message);


            }
        }

        private async Task updateProductAsync()
        {
            var result = await _productService.UpdateProductAsync(_selectedProduct);
            if (result.Success)
            {
                await LoadProductsAsync();
            }
            else
            {
                _errorMessage = "Error updating product: " + result.Message;
            }
        }

        [RelayCommand]
        public async Task DeleteProductAsync()
        {
            if (_selectedProduct == null)
            {
                _errorMessage = "No product selected for deletion.";
                return;
            }
            var result = await _productService.DeleteProductAsync(_selectedProduct.Id);
            if (result.Success)
            {
                await LoadProductsAsync();
            }
            else
            {
                _errorMessage = "Error deleting product: " + result.Message;
            }
        }

        [RelayCommand]
        public async Task NewProductWindow()
        {
            _messageDialogService.ShowMessage("Creating a new product.");
            _productDetailsView.Show();
   
            _productDetailsView.DataContext = this;
            SelectedProduct = new Product(); // Initialize a new product for creation
        }

        [RelayCommand]
        public async Task SaveProductAsync()
        {
            if (_selectedProduct == null)
            {
                _errorMessage = "No product selected for saving.";
                return;
            }
            if (_selectedProduct.Id == 0)
            {
                await createProductAsync();
            }
            else
            {
                await updateProductAsync();
            }
        }

        [RelayCommand]
        public async Task ShowProductDetails(Product product)
        {
            if (product != null)
            {
                SelectedProduct = product;
                _productDetailsView.DataContext = this;
                _productDetailsView.Show();
            }
        }

       


    }
}
