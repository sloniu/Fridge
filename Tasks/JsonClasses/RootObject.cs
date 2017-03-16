using Newtonsoft.Json;

namespace Tasks.JsonClasses
{
    public sealed class RootObject
    {

        [JsonProperty("stream")]
        public Stream stream { get; set; }

        [JsonProperty("_links")]
        public Links3 _links { get; set; }
    }
}