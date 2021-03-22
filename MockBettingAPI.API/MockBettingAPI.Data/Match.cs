using System;
using System.Collections.Generic;

namespace MockBettingAPI.Data
{
    public class Match
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime MatchDate { get; set; }
        public TimeSpan MatchTime { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public Sport Sport { get; set; }

        public virtual ICollection<MatchOdds> MatchOdds { get; set; }
    }
}
