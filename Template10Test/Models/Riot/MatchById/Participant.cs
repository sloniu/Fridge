using System.Collections.Generic;

namespace Template10Test.Models.Riot.MatchById
{
    public class Participant
    {
        public int TeamId { get; set; }
        public int Spell1Id { get; set; }
        public int Spell2Id { get; set; }
        public int ChampionId { get; set; }
        public string HighestAchievedSeasonTier { get; set; }
        public Timeline Timeline { get; set; }
        public List<Mastery> Masteries { get; set; }
        public Stats Stats { get; set; }
        public int ParticipantId { get; set; }
        public List<Rune> Runes { get; set; }
    }
}