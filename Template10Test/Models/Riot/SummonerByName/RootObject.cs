using Newtonsoft.Json;

namespace Template10Test.Models.Riot.SummonerByName
{
    public class RootObject
    {
        [JsonProperty("Summoner")]
        public Summoner Summoner { get; set; }
    }
}