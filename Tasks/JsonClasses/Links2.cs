using Newtonsoft.Json;

namespace Tasks.JsonClasses
{
    public sealed class Links2
    {
        [JsonProperty("self")]
        public string self { get; set; }

        [JsonProperty("follows")]
        public string follows { get; set; }

        [JsonProperty("commercial")]
        public string commercial { get; set; }

        [JsonProperty("stream_key")]
        public string stream_key { get; set; }

        [JsonProperty("chat")]
        public string chat { get; set; }

        [JsonProperty("features")]
        public string features { get; set; }

        [JsonProperty("subscriptions")]
        public string subscriptions { get; set; }

        [JsonProperty("editors")]
        public string editors { get; set; }

        [JsonProperty("teams")]
        public string teams { get; set; }

        [JsonProperty("videos")]
        public string videos { get; set; }
    }
}