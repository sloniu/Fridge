using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10Test.Models.Fridge;

namespace Template10Test.ViewModels
{
    class ProductsPageViewModel : ViewModelBase
    {
        public ProductsPageViewModel()
        {
            DeleteSelectedProduct = new DelegateCommand<object>(OnDeleteSelectedProduct, CanDeleteSelectedProduct);
            AddSelectedProduct = new DelegateCommand<object>(OnAddSelectedProduct, CanAddSelectedProduct);

            using (var db = new RecipeContext())
            {
                Products = db.Products.ToList();
            }
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

        private List<Product> _products;

        public List<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                RaisePropertyChanged(() => Products);
            }
        }

        private string _addProduct;

        public string AddProduct
        {
            get { return _addProduct; }
            set { Set(ref _addProduct, value); }
        }

        private double _addProductAmount;

        public double AddProductAmount
        {
            get { return _addProductAmount; }
            set { Set(ref _addProductAmount, value); }
        }

        public void AddProductButton()
        {
            using (var db = new RecipeContext())
            {
                var product = new Product()
                {
                    Name = $"{AddProduct} - {AddProductAmount}",
                    Size = AddProductAmount
                };
                db.Products.Add(product);
                db.SaveChanges();
                Debug.WriteLine(db.Products.FirstOrDefault(p => p.Name == product.Name));
                Products = db.Products.ToList();
            }

        }

        public ICommand DeleteSelectedProduct { get; set; }

        private void OnDeleteSelectedProduct(object obj)
        {
            using (var db = new RecipeContext())
            {
                var product = SelectedProduct;
                db.Products.Remove(product);
                db.SaveChanges();
                Products = db.Products.ToList();
            }
        }

        private bool CanDeleteSelectedProduct(object obj)
        {
            return true;
        }

        public ICommand AddSelectedProduct { get; set; }

        private void OnAddSelectedProduct(object obj)
        {
            using (var db = new RecipeContext())
            {
                var product = SelectedProduct;
                var updatedProduct = db.Products.FirstOrDefault(p => p.Name == product.Name);
                if (updatedProduct == null) return;
                db.FridgeProducts.Add(new FridgeProduct() { Name = updatedProduct.Name, Size = updatedProduct.Size });
                db.SaveChanges();
            }
        }

        private bool CanAddSelectedProduct(object obj)
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