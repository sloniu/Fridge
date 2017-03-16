using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Template10Test.Models;
using Template10Test.Models.Channel;

namespace Template10Test.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void UIElement_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            Streamer selectedItem = (sender as StackPanel).DataContext as Streamer;
            this.thisGridView.SelectedItem = selectedItem;
            //throw new NotImplementedException();
        }
        
        // todo: make advanced login page with saving credentials 
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        
    }
}
