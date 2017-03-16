using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Template10Test.Models
{
    class TwitchApiClient
    {
        public void GetUserData()
        {
            using (var w = new HttpClient())
            {
                var json =
                    w.GetStringAsync("https://api.twitch.tv/kraken/user/" +
                                     "?client_id=4hz5hgythniudwl0frrequyu6wxbv02").Result;
                var r = JsonConvert.DeserializeObject<string>(json);
            }
        }
    }
}
