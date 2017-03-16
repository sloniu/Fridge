using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.Storage;
using Windows.UI.Notifications;
using NotificationsExtensions;
using NotificationsExtensions.Toasts;

namespace Tasks
{
    class Notification
    {
        private static ApplicationDataContainer _settings = ApplicationData.Current.LocalSettings;
        //private static ObservableCollection<Streamer> _streamers = new ObservableCollection<Streamer>();
        private static StreamCheck _streamCheck = new StreamCheck();

        public static void CreateToast()
        {
            //            if (_streamers.Count == 0)
            //                _streamers = _streamCheck.Streamers();
            string[] arr = ((IEnumerable)_settings.Values["streams"]).Cast<object>()
                                             .Select(x => x.ToString())
                                             .ToArray();
            _streamCheck.Check(arr);
            
            foreach (string st in arr)
            {
                if (_settings.Values[st + "changed"].Equals(true) && (string)_settings.Values[st + "current"] == "Online")
                {
                    _settings.Values[st + "changed"] = false;
                    string title = st + " is now " + _settings.Values[st + "current"];
                    string content = _settings.Values[st + "title"] + "\n" + _settings.Values[st + "game"];
                    string logo = _settings.Values[st + "logo"].ToString();
                    string image = _settings.Values[st + "image"].ToString();

                    ToastContent toastContent = new ToastContent
                    {
                        Visual = new ToastVisual
                        {
                            BindingGeneric = new ToastBindingGeneric
                            {
                                Children =
                                {
                                    new AdaptiveText
                                    {
                                        Text = title
                                    },
                                    new AdaptiveText
                                    {
                                        Text = content
                                    },
                                    new AdaptiveImage
                                    {
                                        Source = image
                                    }
                                },
                                AppLogoOverride = new ToastGenericAppLogo()
                                {
                                    Source = logo,
                                    HintCrop = ToastGenericAppLogoCrop.None
                                }
                            }
                        }
                    };
//                    toastContent.Audio = new ToastAudio()
//                    {
//                        Src = new Uri("ms-appx:///Assets/Audio/CustomToastAudio.m4a")
//                    };
                    toastContent.Scenario = ToastScenario.Reminder;

//                    Construct the actions for the toast (inputs and buttons)
//                    toastContent.Actions = new ToastActionsCustom()
//                    {
//                        Buttons =
//                        {
//                            new ToastButton("Watch", new QueryString()
//                            {
//                                {"action", "openStream"}
//                            }.ToString())                            
//                        }
//                    };

                    // And create the toast notification
                    var toast = new ToastNotification(toastContent.GetXml());
                    toast.ExpirationTime = DateTime.Now.AddHours(3);
                    ToastNotificationManager.CreateToastNotifier().Show(toast);
                    
                    Debug.WriteLine(st + " (after toast) changed : " + _settings.Values[st + "changed"]);
                }
            }
        }
    }
}

