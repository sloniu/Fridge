using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template10Test.Models
{
    class Build
    {
        public int BuildId { get; set; }
        public string PlayerName { get; set; }
        public int ChampionId { get; set; }
        public int Item1Id { get; set; }
        public int Item2Id { get; set; }
        public int Item3Id { get; set; }
        public int Item4Id { get; set; }
        public int Item5Id { get; set; }
        public int Item6Id { get; set; }
        public int Spell1Id { get; set; }
        public int Spell2Id { get; set; }
        public long MatchId { get; set; }
    }
}
