using Newtonsoft.Json;

namespace Template10Test.Models.Channel
{
    public class RootObject
    {
        [JsonProperty("mature")]
        public object mature { get; set; }

        [JsonProperty("status")]
        public object status { get; set; }

        [JsonProperty("broadcaster_language")]
        public object broadcaster_language { get; set; }

        [JsonProperty("display_name")]
        public object display_name { get; set; }

        [JsonProperty("game")]
        public object game { get; set; }

        [JsonProperty("language")]
        public object language { get; set; }

        [JsonProperty("_id")]
        public object _id { get; set; }

        [JsonProperty("name")]
        public object name { get; set; }

        [JsonProperty("created_at")]
        public object created_at { get; set; }

        [JsonProperty("updated_at")]
        public object updated_at { get; set; }

        [JsonProperty("delay")]
        public object delay { get; set; }

        [JsonProperty("logo")]
        public object logo { get; set; }

        [JsonProperty("banner")]
        public object banner { get; set; }

        [JsonProperty("video_banner")]
        public string video_banner { get; set; }

        [JsonProperty("background")]
        public object background { get; set; }

        [JsonProperty("profile_banner")]
        public string profile_banner { get; set; }

        [JsonProperty("profile_banner_background_color")]
        public object profile_banner_background_color { get; set; }

        [JsonProperty("partner")]
        public bool partner { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("views")]
        public int views { get; set; }

        [JsonProperty("followers")]
        public int followers { get; set; }

        [JsonProperty("_links")]
        public Links _links { get; set; }
    }
}
