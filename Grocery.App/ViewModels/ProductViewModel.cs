using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        public ObservableCollection<Product> Products { get; set; }

        public ProductViewModel(IProductService productService)
        {
            _productService = productService;
            Products = [];
            foreach (Product p in _productService.GetAll()) Products.Add(p);
        }

        [RelayCommand]
        public async Task GoToNewProductPage()
        {
            await Shell.Current.GoToAsync(nameof(NewProductView), true);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            Products.Clear();
            foreach (Product p in _productService.GetAll()) Products.Add(p);
        }
    }
}