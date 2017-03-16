using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Windows.Storage;
using Newtonsoft.Json;
using Tasks.JsonClasses;

namespace Tasks
{
    class StreamCheck
    {
        public ApplicationDataContainer Settings = ApplicationData.Current.LocalSettings;

        public void Check(string[] arr)
        {
            using (var w = new HttpClient())
            {
                foreach (string st in arr)
                {
                    var json = w.GetStringAsync("https://api.twitch.tv/kraken/streams/" + st + "?client_id=4hz5hgythniudwl0frrequyu6wxbv02").Result;
                    RootObject r = JsonConvert.DeserializeObject<RootObject>(json);
                    var temp = r.stream == null ? "Offline" : "Online";
                    Debug.WriteLine("before change");
                    Debug.WriteLine(st + " prev : " + Settings.Values[st + "previous"]);
                    Debug.WriteLine(st + " curr : " + Settings.Values[st + "current"]);
                    Debug.WriteLine(st + " changed : " + Settings.Values[st + "changed"]);
                    Debug.WriteLine("=============================================");

                    if (Settings.Values[st + "current"].ToString() == temp) continue;
                    if (temp == "Online")
                    {
                        Settings.Values[st + "title"] = r.stream.channel.status;
                        Settings.Values[st + "game"] = r.stream.game;
                        Settings.Values[st + "image"] = r.stream.preview.medium;
                        Settings.Values[st + "logo"] = r.stream.channel.logo;
                    }
                    
                    Settings.Values[st + "previous"] = Settings.Values[st + "current"];
                    Settings.Values[st + "current"] = temp;
                    Settings.Values[st + "changed"] = true;
                    Debug.WriteLine("after change");
                    Debug.WriteLine(st + " prev : " + Settings.Values[st + "previous"]);
                    Debug.WriteLine(st + " curr : " + Settings.Values[st + "current"]);
                    Debug.WriteLine(st + " changed : " + Settings.Values[st + "changed"]);
                }
            }
        }
    }
}
