using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10Test.Models;
using Template10Test.Views;

namespace Template10Test.ViewModels
{
    public class BuildsPageViewModel : ViewModelBase
    {
        public BuildsPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        string _Value = "Gas";

        public string Value
        {
            get { return _Value; }
            set { Set(ref _Value, value); }
        }

        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                RaisePropertyChanged(() => PlayerName);
            }
        }

        private string _region;

        public string Region
        {
            get { return _region; }
            set
            {
                _region = value;
                RaisePropertyChanged(() => Region);
            }
        }

        private string _builds;

        public string Builds
        {
            get { return _builds; }
            set
            {
                _builds = value;
                RaisePropertyChanged(() => Builds);
            }
        }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode,
           IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
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

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(SettingsPage), 2);

        public void FetchBuilds()
        {
            if (PlayerName != null)
            {
                using (var w = new HttpClient())
                {
                    try
                    {
                        var json = w.GetStringAsync($"http://localhost:51568/api/test/{Region}/{PlayerName}").Result;
                        var build = JsonConvert.DeserializeObject<Build>(json);
                        Builds = build.MatchId.ToString();
                    }
                    catch (Exception e)
                    {

                    }
                    
                }
            }
        }

        private string _matchId;

        public string MatchId
        {
            get { return _matchId; }
            set
            {
                _matchId = value;
                RaisePropertyChanged(() => MatchId);
            }
        }


        public void PushBuild()
        {
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        { "region", "asdf" },
                        { "summonerName", "asdf" }
                    };

                    var content = new FormUrlEncodedContent(values);
                    try
                    {
                        var response =
                            client.PostAsync(
                                $"http://localhost:51568/api/Builds/{LoginManager.Instance.Token}/{Region}/{PlayerName}/{MatchId}",
                                content).Result;
                    }
                    catch (Exception e)
                    {
                        //System.Console.WriteLine(e);
                        //throw;
                    }

                    //var responseString = await response.Content.ReadAsStringAsync();

                    //var content = new StringContent(b.ToString(), Encoding.UTF8,"application/json");;
                    //var response = await client.PostAsync("asdf", b);
                }
        }

        ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
    }
}
