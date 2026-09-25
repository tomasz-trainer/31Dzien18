using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared;
using P06Shop.Shared.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Text;

namespace P12MAUI.Client.ViewModels
{
    [QueryProperty(nameof(Product), nameof(Product))]
    [QueryProperty(nameof(ProductsViewModel), nameof(ProductsViewModel))]
    public partial class ProductDetailsViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IMeesageDialogService _messageDialogService;
        private readonly ProductsViewModel _productsViewModel;

        [ObservableProperty]
        private Product _product;

        public ProductDetailsViewModel(IProductService productService,
             IMeesageDialogService messageDialogService)
        {
            _productService = productService;
            _messageDialogService = messageDialogService;
        }

        [RelayCommand]
        public async Task Save()
        {
            if (_product.Id > 0)
            {
                await updateProductAsync();
            }
            else
            {
                await createProductAsync();
            }
             
        }

        private async Task createProductAsync()
        {
            var result = await _productService.CreateProductAsync(_product);
            if (result.Success)
            {
                await _productsViewModel.LoadProductsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage("Error creating product: " + result.Message);
            }
        }

        private async Task updateProductAsync()
        {
            var result = await _productService.UpdateProductAsync(_product);
            if (result.Success)
            {
                await _productsViewModel.LoadProductsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage("Error creating product: " + result.Message);
            }
        }
    }
}
