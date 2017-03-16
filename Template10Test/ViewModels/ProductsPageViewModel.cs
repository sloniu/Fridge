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
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }

            DeleteSelectedProduct = new DelegateCommand<object>(OnDeleteSelectedProduct, CanDeleteSelectedProduct);
            AddSelectedProduct = new DelegateCommand<object>(OnAddSelectedProduct, CanAddSelectedProduct);

            Products = new ObservableCollection<Product>()
            {
                new Product() { Name = "Mleko" },
                new Product() { Name = "Pomidor" },
                new Product() { Name = "Mas³o" },
                new Product() { Name = "Ser ¿ó³ty" },
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

            var obj = new Product()
            {
                Name = $"{AddProduct} - {AddProductAmount}",
                Amount = AddProductAmount
            };
            Products.Add(obj);

            //            try
            //            {
            //                if (IsLoading) return;
            //                using (var w = new HttpClient())
            //                {
            //                    var json = w.GetStringAsync("https://api.twitch.tv/kraken/channels/" + AddStreamer + "?client_id=4hz5hgythniudwl0frrequyu6wxbv02").Result;
            //                    var r = JsonConvert.DeserializeObject<RootObject>(json);
            //                    var temp = (settings.Values["streams"] as Array).OfType<string>().ToList();
            //                    if (temp.Contains(r.name.ToString())) return;
            //                    var obj = new Streamer()
            //                    {
            //                        DisplayName = r.display_name.ToString(),
            //                        Name = r.name.ToString(),
            //                        Logo = (string)r.logo,
            //                        Link = r.url
            //                    };
            //                    Streamers.Add(obj);
            //
            //                    temp.Add(AddStreamer);
            //                    //Data.Streams = temp.ToArray();
            //                    settings.Values["streams"] = temp.ToArray();
            //                    settings.Values[AddStreamer + "previous"] = "Offline";
            //                    settings.Values[AddStreamer + "current"] = "Offline";
            //                    settings.Values[AddStreamer + "changed"] = false;
            //                    Debug.WriteLine("\nstreamer added\n");
            //                    SortStreamers();
            //                }
            //            }
            //            catch (Exception)
            //            {
            //                await new MessageDialog("Streamer " + AddStreamer + " not found.").ShowAsync();
            //            }
            //            finally
            //            {
            //                IsLoading = false;
            //            }
        }

        //        public bool IsLoading { get; set; } = false;

        public ICommand DeleteSelectedProduct { get; set; }

        private void OnDeleteSelectedProduct(object obj)
        {
            var product = SelectedProduct;
            Debug.WriteLine($"Deleted {SelectedProduct.Name}");
            Products.Remove(product);
        }

        private bool CanDeleteSelectedProduct(object obj)
        {
            return true;
        }

        public ICommand AddSelectedProduct { get; set; }

        private void OnAddSelectedProduct(object obj)
        {
            var product = SelectedProduct;
            Debug.WriteLine($"Added {SelectedProduct.Name}");
            //TODO: dodawanie do LODÓWKI a nie produktów
            Products.Add(product);
        }

        private bool CanAddSelectedProduct(object obj)
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