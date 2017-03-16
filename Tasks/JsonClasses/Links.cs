using Newtonsoft.Json;

namespace Tasks.JsonClasses
{
    public sealed class Links
    {
        [JsonProperty("self")]
        public string self { get; set; }
    }
}