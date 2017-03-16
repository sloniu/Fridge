using Newtonsoft.Json;

namespace WebApi.Models.Token
{
    public class Token
    {
        public bool Valid { get; set; }
        public Authorization Authorization { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}


