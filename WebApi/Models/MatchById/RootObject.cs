using System.Collections.Generic;

namespace WebApi.Models.MatchById
{
    public class RootObject
    {
        public long MatchId { get; set; }
        public string Region { get; set; }
        public string PlatformId { get; set; }
        public string MatchMode { get; set; }
        public string MatchType { get; set; }
        public long MatchCreation { get; set; }
        public int MatchDuration { get; set; }
        public string QueueType { get; set; }
        public int MapId { get; set; }
        public string Season { get; set; }
        public string MatchVersion { get; set; }
        public List<Participant> Participants { get; set; }
        public List<ParticipantIdentity> ParticipantIdentities { get; set; }
        public List<Team> Teams { get; set; }
    }
}