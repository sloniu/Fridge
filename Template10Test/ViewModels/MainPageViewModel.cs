using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10Test.Models;
using Template10Test.Models.Channel;
using Template10Test.Views;

//using AppNotifications;

namespace Template10Test.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
            Streamers = FillStreamers();
            DeleteSelectedStreamer = new DelegateCommand<object>(OnDeleteSelectedStreamer, CanDeleteSelectedStreamer);
        }

        string _Value = "Gas";

        public string Value
        {
            get { return _Value; }
            set { Set(ref _Value, value); }
        }

#region navigation
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
        #endregion

#region secondary commands
        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(SettingsPage), 2);
#endregion

        ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;


        private string _addStreamer;

        public string AddStreamer
        {
            get { return _addStreamer; }
            set { Set(ref _addStreamer, value); }
        }

        private Streamer _selectedStreamer;

        public Streamer SelectedStreamer
        {
            get { return _selectedStreamer; }
            set
            {
                _selectedStreamer = value;
                RaisePropertyChanged(() => SelectedStreamer);
            }
        }

        public async void AddStreamerButton()
        {
            try
            {
                if (IsLoading) return;
                using (var w = new HttpClient())
                {
                    var json = w.GetStringAsync("https://api.twitch.tv/kraken/channels/" + AddStreamer + "?client_id=4hz5hgythniudwl0frrequyu6wxbv02").Result;
                    var r = JsonConvert.DeserializeObject<RootObject>(json);
                    var temp = (settings.Values["streams"] as Array).OfType<string>().ToList();
                    if (temp.Contains(r.name.ToString())) return;
                    var obj = new Streamer()
                    {
                        DisplayName = r.display_name.ToString(),
                        Name = r.name.ToString(),
                        Logo = (string) r.logo,
                        Link = r.url
                    };
                    Streamers.Add(obj);

                    temp.Add(AddStreamer);
                    //Data.Streams = temp.ToArray();
                    settings.Values["streams"] = temp.ToArray();
                    settings.Values[AddStreamer + "previous"] = "Offline";
                    settings.Values[AddStreamer + "current"] = "Offline";
                    settings.Values[AddStreamer + "changed"] = false;
                    Debug.WriteLine("\nstreamer added\n");
                    SortStreamers();
                }
            }
            catch (Exception)
            {
                await new MessageDialog("Streamer " + AddStreamer + " not found.").ShowAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public bool IsLoading { get; set; } = false;

        // todo: figure out why detects entries after filling second time
        public async void LoadStreamers()
        {
            
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                int userId;
                using (var w = new HttpClient())
                {
                    w.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
                    w.DefaultRequestHeaders.Add("Client-ID", "4hz5hgythniudwl0frrequyu6wxbv02");
                    w.DefaultRequestHeaders.Add("Authorization", "OAuth " + LoginManager.Instance.Token);
                    var json = await w.GetStringAsync("https://api.twitch.tv/kraken/user");
                    var r = JsonConvert.DeserializeObject<Models.User.RootObject>(json);
                    userId = r._Id;
                }
                using (var w = new HttpClient())
                {
                    w.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
                    w.DefaultRequestHeaders.Add("Client-ID", "4hz5hgythniudwl0frrequyu6wxbv02");
                    var json = w.GetStringAsync($"https://api.twitch.tv/kraken/users/{userId}/follows/channels").Result;
                    var r = JsonConvert.DeserializeObject<Models.Users.RootObject>(json);
                    var follows = r.follows;
                    foreach (var follow in follows)
                    {
                        var temp = !(settings.Values["streams"] is Array)
                            ? new List<string>()
                            : ((Array)settings.Values["streams"]).OfType<string>().ToList();
                        if (temp.Contains(follow.Channel.name)) continue;
                        var obj = new Streamer()
                        {
                            DisplayName = follow.Channel.display_name,
                            Name = follow.Channel.name,
                            Logo = (string)follow.Channel.logo,
                            Link = follow.Channel.url
                        };
                        Streamers.Add(obj);
                        temp.Add(follow.Channel.name);
                        //Data.Streams = temp.ToArray();
                        settings.Values["streams"] = temp.ToArray();
                        settings.Values[follow.Channel.name + "previous"] = "Offline";
                        settings.Values[follow.Channel.name + "current"] = "Offline";
                        settings.Values[follow.Channel.name + "changed"] = false;
                        Debug.WriteLine("\nstreamer added\n");
                        SortStreamers();
                    }
                }
            }
            catch (Exception e)
            {
                await new MessageDialog(e.Message).ShowAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private ObservableCollection<Streamer> _streamers;

        public ObservableCollection<Streamer> Streamers
        {
            get { return _streamers; }
            set { Set(ref _streamers, value); }
        }

        public ObservableCollection<Streamer> FillStreamers()
        {
            var col = new ObservableCollection<Streamer>();
            using (var w = new HttpClient())
            {
                if (settings.Values["streams"] == null)
                {
                    string[] streams =
                    {
                        ""
                    };
                    settings.Values["streams"] = streams;
                }
                try
                {
                    foreach (string st in settings.Values["streams"] as Array)
                    {
                        try
                        {
                            var json = w.GetStringAsync("https://api.twitch.tv/kraken/channels/" + st + "?client_id=4hz5hgythniudwl0frrequyu6wxbv02").Result;
                            RootObject r = JsonConvert.DeserializeObject<RootObject>(json);
                            var obj = new Streamer()
                            {
                                DisplayName = r.display_name.ToString(),
                                Name = st,
                                Logo = (string) r.logo,
                                Link = r.url,
                                State = (string)settings.Values[st + "current"]
                            };
                            col.Add(obj);
                        }
                        catch (AggregateException e)
                        {
                            Debug.WriteLine(e.Message);
                            Debug.WriteLine(st);
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            return col;
        }

        public async void ClickCommand(object sender, object parameter)
        {
            var arg = parameter as Windows.UI.Xaml.Controls.ItemClickEventArgs;
            var item = arg.ClickedItem as Streamer;
            var uri = new Uri(item.Link);
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

        public void SortStreamers()
        {
            ObservableCollection<Streamer> temp = new ObservableCollection<Streamer>(Streamers.OrderBy(a => a.Name));
            Streamers = temp;
            List<string> list = new List<string>();
            foreach (var s in Streamers)
            {
                list.Add(s.Name);
            }
            settings.Values["streams"] = list.ToArray();
        }

        #region background task
        public async void Register()
        {
            string taskName = "myBgTask";

            foreach (var cur in BackgroundTaskRegistration.AllTasks)
                if (cur.Value.Name == taskName)
                {
                    await (new MessageDialog("Task alrdy registared.")).ShowAsync();
                    return;
                }
            BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();

            taskBuilder.Name = "myBgTask";
            taskBuilder.TaskEntryPoint = "Tasks.BgTask";

            taskBuilder.SetTrigger(new TimeTrigger(15, false));
            var access = await BackgroundExecutionManager.RequestAccessAsync();
            if (access == BackgroundAccessStatus.Denied)
                return;

            taskBuilder.Register();

            await (new MessageDialog("task registered")).ShowAsync();

            Debug.WriteLine("tostek");
        }

        public void Unregister()
        {
            string taskName = "myBgTask";

            foreach (var cur in BackgroundTaskRegistration.AllTasks)
                if (cur.Value.Name == taskName)
                {
                    cur.Value.Unregister(true);
                }
        }
#endregion

        public ICommand DeleteSelectedStreamer { get; set; }

        private void OnDeleteSelectedStreamer(object obj)
        {
            var streamer = SelectedStreamer;
            Debug.WriteLine("");
            Streamers.Remove(streamer);
            List<string> temp = (settings.Values["streams"] as Array).OfType<string>().ToList();
            temp.Remove(streamer.Name);
            if (temp.Any())
            {
                settings.Values["streams"] = temp.ToArray();
            }
            else
                settings.Values["streams"] = "";
        }

        private bool CanDeleteSelectedStreamer(object obj)
        {
            return true;
        }


        public ICommand ResetSettings { get; set; }

        public async void OnResetSettings()
        {
            //            ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
            //            settings.DeleteContainer(settings.Name);
            await ApplicationData.Current.ClearAsync();
            Streamers = new ObservableCollection<Streamer>();
            //            foreach (var container in ApplicationData.Current.LocalSettings.Containers)
            //            {
            //                
            //            }
        }

        private bool CanResetSettings(object obj)
        {
            return true;
        }

        public void Login()
        {
            var manager = LoginManager.Instance;
            manager.Login();
        }

        public void RefreshStreamers()
        {
            foreach (var streamer in Streamers)
            {
                if (ReferenceEquals(settings.Values[streamer.Name + "current"], "Online"))
                {
                    streamer.State = "Online";
                }
                else if (ReferenceEquals(settings.Values[streamer.Name + "current"], "Offline"))
                {
                    streamer.State = "Offline";
                }
            }
        }
    }
}

