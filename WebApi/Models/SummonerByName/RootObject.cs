using Newtonsoft.Json;

namespace WebApi.Models.SummonerByName
{
    public class RootObject
    {
        [JsonProperty("Summoner")]
        public Summoner Summoner { get; set; }
    }
}