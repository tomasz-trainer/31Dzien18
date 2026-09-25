using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared;
using P06Shop.Shared.Services.CategoryService;
using P06Shop.Shared.Services.ProductService;
using System.Collections.ObjectModel;
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
        private readonly ICategoryService _categoryService;
        private readonly IMeesageDialogService _messageDialogService;
        
        private ProductsViewModel _productsViewModel;

        public ProductsViewModel ProductsViewModel
        {
            get => _productsViewModel;
            set => _productsViewModel = value;
        }

        [ObservableProperty]
        private Product _product;

        [ObservableProperty]
        private ObservableCollection<Category> _categories = new();

        [ObservableProperty]
        private Category? _selectedCategory;

        private static readonly Category NoCategory = new Category { Id = 0, Name = "No category" };

        public ProductDetailsViewModel(IProductService productService,
             ICategoryService categoryService,
             IMeesageDialogService messageDialogService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _messageDialogService = messageDialogService;
        }

        public async Task LoadCategoriesAsync(int? categoryIdToSelect = null)
        {
            var response = await _categoryService.GetCategoriesAsync();
            if (!response.Success || response.Data == null)
            {
                _messageDialogService.ShowMessage("Error loading categories: " + response.Message);
                return;
            }

            var categories = new ObservableCollection<Category> { NoCategory };
            foreach (var category in response.Data)
            {
                categories.Add(category);
            }
            Categories = categories;

            var selectedId = categoryIdToSelect ?? Product?.CategoryId ?? 0;
            SelectedCategory = Categories.FirstOrDefault(c => c.Id == selectedId) ?? NoCategory;
        }

        [RelayCommand]
        public async Task AddCategory()
        {
            var name = await Shell.Current.DisplayPromptAsync("New category", "Category name:", "Add", "Cancel", maxLength: 50);
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var result = await _categoryService.CreateCategoryAsync(new Category { Name = name.Trim() });
            if (result.Success && result.Data != null)
            {
                Product.CategoryId = result.Data.Id;
                await LoadCategoriesAsync(result.Data.Id);
            }
            else
            {
                _messageDialogService.ShowMessage("Error creating category: " + result.Message);
            }
        }

        [RelayCommand]
        public async Task Save()
        {
            Product.CategoryId = SelectedCategory == null || SelectedCategory.Id == 0
                ? null
                : SelectedCategory.Id;

            if (_product.Id > 0)
            {
                await updateProductAsync();
            }
            else
            {
                await createProductAsync();
            }
            await Shell.Current.GoToAsync(".."); // Navigate back to the previous page

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


        [RelayCommand]
        public async Task DeleteAsync()
        {
            if (_product == null)
            {
                var errorMessage = "No product selected for deletion.";
                _messageDialogService.ShowMessage(errorMessage);
                return;
            }
            var result = await _productService.DeleteProductAsync(_product.Id);
            if (result.Success)
            {
                await _productsViewModel.LoadProductsAsync();
                
            }
            else
            {
                _messageDialogService.ShowMessage("Error deleting product: " + result.Message);
            }
        }
    }
}
