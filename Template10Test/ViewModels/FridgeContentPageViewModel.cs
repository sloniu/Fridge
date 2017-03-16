using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10Test.Models.Fridge;
using Template10Test.Views;

namespace Template10Test.ViewModels
{
    class FridgeContentPageViewModel : ViewModelBase
    {
        public FridgeContentPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }

            DeleteSelectedProduct = new DelegateCommand<object>(OnDeleteSelectedProduct, CanDeleteSelectedProduct);

            Products = new ObservableCollection<Product>()
            {
                new Product() { Name = "Mleko" },
                new Product() { Name = "Pomidor" },
                new Product() { Name = "Masło" },
                new Product() { Name = "Ser żółty" },
            };
        }

        string _Value = "Gas";

        public string Value
        {
            get { return _Value; }
            set { Set(ref _Value, value); }
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                RaisePropertyChanged(() => SelectedProduct);
            }
        }

        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set {
                _products = value;
                RaisePropertyChanged(() => Products);
            }
        }

        public ICommand DeleteSelectedProduct { get; set; }

        private void OnDeleteSelectedProduct(object obj)
        {
            var product = SelectedProduct;
            Debug.WriteLine($"deleted{SelectedProduct.Name}");
            Products.Remove(product);
//            List<string> temp = (settings.Values["streams"] as Array).OfType<string>().ToList();
//            temp.Remove(streamer.Name);
//            if (temp.Any())
//            {
//                settings.Values["streams"] = temp.ToArray();
//            }
//            else
//                settings.Values["streams"] = "";
        }

        private bool CanDeleteSelectedProduct(object obj)
        {
            return true;
        }


        public async void ClickCommand(object sender, object parameter)
        {
            var arg = parameter as Windows.UI.Xaml.Controls.ItemClickEventArgs;
            var item = arg.ClickedItem;
            var uri = new Uri(item as string);
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            if (success)
            {
                // URI launched
            }
            else
            {
                // URI launch failed
            }
        }

        #region Navigations
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Value = (suspensionState.ContainsKey(nameof(Value))) ? suspensionState[nameof(Value)]?.ToString() : parameter?.ToString();
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }
        #endregion

    }
}
