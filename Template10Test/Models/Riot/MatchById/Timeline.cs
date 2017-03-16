namespace Template10Test.Models.Riot.MatchById
{
    public class Timeline
    {
        public CreepsPerMinDeltas CreepsPerMinDeltas { get; set; }
        public XpPerMinDeltas XpPerMinDeltas { get; set; }
        public GoldPerMinDeltas GoldPerMinDeltas { get; set; }
        public CsDiffPerMinDeltas CsDiffPerMinDeltas { get; set; }
        public XpDiffPerMinDeltas XpDiffPerMinDeltas { get; set; }
        public DamageTakenPerMinDeltas DamageTakenPerMinDeltas { get; set; }
        public DamageTakenDiffPerMinDeltas DamageTakenDiffPerMinDeltas { get; set; }
        public string Role { get; set; }
        public string Lane { get; set; }
    }
}