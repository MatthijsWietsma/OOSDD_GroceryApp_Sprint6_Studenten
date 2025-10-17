using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;


namespace Grocery.App.ViewModels
{
    public partial class NewProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly Client _client;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private int stock = 0;

        [ObservableProperty]
        private DateOnly shelfLife = DateOnly.FromDateTime(DateTime.Today);

        [ObservableProperty]
        private Decimal price = 0m;

        [ObservableProperty]
        private string infoMessage = string.Empty;

        public NewProductViewModel(IProductService productService, GlobalViewModel global)
        {
            _productService = productService;
            _client = global.Client;
        }

        [RelayCommand]
        public void AddProduct()
        {
            if (_client.Role != Role.Admin)
            {
                InfoMessage = "Alleen gebruikers met admin-rol mogen producten aanmaken.";
                return;
            }


            if (String.IsNullOrWhiteSpace(Name))
            {
                InfoMessage = "Alle velden moeten gevuld zijn.";
                return;
            }


            try
            {
                Product p = _productService.Add(new Product(-1, Name, Stock, ShelfLife, Price));
                if (p.Id == -1)
                {
                    InfoMessage = "Er is iets mis gegaan met het toevoegen van het product.";
                    return;
                }

                Name = string.Empty;
                Stock = 0;
                ShelfLife = DateOnly.FromDateTime(DateTime.Today);
                Price = 0;

                InfoMessage = "Product succesvol aangemaakt!";
            }
            catch (Exception e)
            {
                InfoMessage = "Er is iets mis gegaan met het toevoegen van het product.";
            }
        }

    }
}