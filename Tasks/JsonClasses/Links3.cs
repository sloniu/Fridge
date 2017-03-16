using Newtonsoft.Json;

namespace Tasks.JsonClasses
{
    public sealed class Links3
    {

        [JsonProperty("self")]
        public string self { get; set; }

        [JsonProperty("channel")]
        public string channel { get; set; }
    }
}