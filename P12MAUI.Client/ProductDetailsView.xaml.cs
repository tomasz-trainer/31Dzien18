using P12MAUI.Client.ViewModels;

namespace P12MAUI.Client;

public partial class ProductDetailsView : ContentPage
{
    private readonly ProductDetailsViewModel _viewModel;

	public ProductDetailsView(ProductDetailsViewModel productDetailsViewModel)
	{
        _viewModel = productDetailsViewModel;
        BindingContext = productDetailsViewModel;
        InitializeComponent();
		
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCategoriesAsync();
    }
}