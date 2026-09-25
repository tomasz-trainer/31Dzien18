using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared;
using P06Shop.Shared.Services.CategoryService;
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
        private readonly ICategoryService _categoryService;
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

        [ObservableProperty]
        private ObservableCollection<ProductListItem> _productItems = new();

        [ObservableProperty]
        private ObservableCollection<Category> _filterCategories = new();

        [ObservableProperty]
        private Category? _selectedFilterCategory;

        private List<Category> _categories = new();

        private static readonly Category AllCategories = new Category { Id = 0, Name = "All" };

        public ProductsViewModel(IProductService productService,
            ICategoryService categoryService,
            ProductDetailsView productDetailsView, 
            IMeesageDialogService meesageDialogService,
            IConnectivity connectivity,
            IGeolocation geolocation,
            IMap map
            )
        {
            _productService = productService;
            _categoryService = categoryService;
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

            await LoadCategoriesAsync();

            var response = await _productService.GetProductsAsync();
            if (response.Success && response.Data != null)
            {
                
                Products = new ObservableCollection<Product>(response.Data);
                ApplyCategoryFilter();
            }
            else
            {
                // Handle error (e.g., log it, show a message to the user, etc.)
            //    Products = new ObservableCollection<Product>();
            }
        }

      
          

        private async Task LoadCategoriesAsync()
        {
            var response = await _categoryService.GetCategoriesAsync();
            if (!response.Success || response.Data == null)
            {
                _messageDialogService.ShowMessage("Error loading categories: " + response.Message);
                return;
            }

            _categories = response.Data;

            var selectedId = SelectedFilterCategory?.Id ?? 0;
            var filterCategories = new ObservableCollection<Category> { AllCategories };
            foreach (var category in _categories)
            {
                filterCategories.Add(category);
            }
            FilterCategories = filterCategories;
            SelectedFilterCategory = FilterCategories.FirstOrDefault(c => c.Id == selectedId) ?? AllCategories;
        }

        partial void OnSelectedFilterCategoryChanged(Category? value)
        {
            ApplyCategoryFilter();
        }

        private void ApplyCategoryFilter()
        {
            if (Products == null)
            {
                ProductItems = new ObservableCollection<ProductListItem>();
                return;
            }

            var categoryId = SelectedFilterCategory?.Id ?? 0;
            var filtered = categoryId == 0
                ? Products
                : Products.Where(p => p.CategoryId == categoryId);

            ProductItems = new ObservableCollection<ProductListItem>(
                filtered.Select(p => new ProductListItem(p, GetCategoryName(p.CategoryId))));
        }

        private string GetCategoryName(int? categoryId)
        {
            if (categoryId == null)
            {
                return "No category";
            }
            return _categories.FirstOrDefault(c => c.Id == categoryId)?.Name ?? "No category";
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
