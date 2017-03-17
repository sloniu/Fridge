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
            DeleteSelectedProduct = new DelegateCommand<object>(OnDeleteSelectedProduct, CanDeleteSelectedProduct);

            using (var db = new RecipeContext())
            {
                Products = db.FridgeProducts.ToList();
            }
        }

        private FridgeProduct _selectedProduct;

        public FridgeProduct SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                RaisePropertyChanged(() => SelectedProduct);
            }
        }

        private List<FridgeProduct> _products;

        public List<FridgeProduct> Products
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
            using (var db = new RecipeContext())
            {
                var product = SelectedProduct;
                db.FridgeProducts.Remove(product);
                db.SaveChanges();
                Products = db.FridgeProducts.ToList();
            }
        }

        private bool CanDeleteSelectedProduct(object obj)
        {
            return true;
        }


        public async void ClickCommand(object sender, object parameter)
        {
//            var arg = parameter as Windows.UI.Xaml.Controls.ItemClickEventArgs;
//            var item = arg.ClickedItem;
//            var uri = new Uri(item as string);
//            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
//            if (success)
//            {
//                // URI launched
//            }
//            else
//            {
//                // URI launch failed
//            }
        }

        #region Navigations
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
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
