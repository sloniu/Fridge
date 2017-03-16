using Newtonsoft.Json;

namespace Tasks.JsonClasses
{
    public sealed class Stream
    {

        [JsonProperty("_id")]
        public long _id { get; set; }

        [JsonProperty("game")]
        public string game { get; set; }

        [JsonProperty("viewers")]
        public int viewers { get; set; }

        [JsonProperty("created_at")]
        public string created_at { get; set; }

        [JsonProperty("video_height")]
        public int video_height { get; set; }

        [JsonProperty("average_fps")]
        public double average_fps { get; set; }

        [JsonProperty("delay")]
        public int delay { get; set; }

        [JsonProperty("is_playlist")]
        public bool is_playlist { get; set; }

        [JsonProperty("_links")]
        public Links _links { get; set; }

        [JsonProperty("preview")]
        public Preview preview { get; set; }

        [JsonProperty("channel")]
        public Channel channel { get; set; }
    }
}