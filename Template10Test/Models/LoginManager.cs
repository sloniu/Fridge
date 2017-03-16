using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Newtonsoft.Json;
using Template10Test.Models.Channel;

namespace Template10Test.Models
{
    public sealed class LoginManager
    {
        private static volatile LoginManager instance;
        private static object syncRoot = new Object();

        public string ClientId { get; set; } = "4hz5hgythniudwl0frrequyu6wxbv02";
        public string RedirectUri { get; set; } = "http://localhost";
        public string Scope { get; set; } = "user_read+channel_read";
        public string EndUrl { get; set; } = "http://localhost";
        public string Response { get; set; } = "token";
        public string Result { get; set; } = "";
        public string Token { get; set; }

        private LoginManager() { }

        public static LoginManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new LoginManager();
                    }
                }

                return instance;
            }
        }

        public async Task Login()
        {
            Debug.WriteLine(
                $"https://api.twitch.tv/kraken/oauth2/authorize?client_id={ClientId}&redirect_uri={RedirectUri}&scope={Scope}&response_type={Response}");

            Uri startUri = new Uri($"https://api.twitch.tv/kraken/oauth2/authorize?client_id={ClientId}&redirect_uri={RedirectUri}&scope={Scope}&response_type={Response}");
            Uri endUri = new Uri(EndUrl);
            try
            {
                WebAuthenticationResult webAuthenticationResult =
                    await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);

                switch (webAuthenticationResult.ResponseStatus)
                {
                    // Successful authentication. 
                    case WebAuthenticationStatus.Success:
                        Debug.WriteLine("Auth:Success");
                        Token =
                            webAuthenticationResult.ResponseData.Substring(
                                webAuthenticationResult.ResponseData.IndexOf("token=", StringComparison.Ordinal) + 6).Replace("&scope=" + Scope,"");
                        Debug.WriteLine($"Token: {Token}");

//                        using (var w = new HttpClient())
//                        {
//                            var json = w.GetStringAsync("https://api.twitch.tv/kraken/users/sloniu6/follows/channels" + "?client_id=4hz5hgythniudwl0frrequyu6wxbv02").Result;
//                            var r = JsonConvert.DeserializeObject<Models.User.RootObject>(json);
//                            var follows = r.follows;
//                            
//                        }

                        Debug.WriteLine(WebAuthenticationBroker.GetCurrentApplicationCallbackUri());
                        break;
                    // HTTP error. 
                    case WebAuthenticationStatus.ErrorHttp:
                        Result = webAuthenticationResult.ResponseErrorDetail.ToString();
                        Debug.WriteLine("Auth:ErrorHttp");
                        break;
                    default:
                        Result = webAuthenticationResult.ResponseData;
                        Debug.WriteLine("Auth:DefaultCase");
                        break;
                }
                // wbVewPostAuthentication is a webPane
                //if (!string.IsNullOrWhiteSpace(result)) wbVewPostAuthentication.NavigateToString(result);
                
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }
        }
    }
}
