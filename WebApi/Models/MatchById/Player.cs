namespace WebApi.Models.MatchById
{
    public class Player
    {
        public int SummonerId { get; set; }
        public string SummonerName { get; set; }
        public string MatchHistoryUri { get; set; }
        public int ProfileIcon { get; set; }
    }
}