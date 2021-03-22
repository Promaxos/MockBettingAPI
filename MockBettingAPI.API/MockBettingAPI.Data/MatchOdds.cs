using System;
using System.Collections.Generic;
using System.Text;

namespace MockBettingAPI.Data
{
    public class MatchOdds
    {
        public int ID { get; set; }
        public int MatchID { get; set; }
        public Match Match { get; set; }
        public string Specifier { get; set; }
        public decimal Odd { get; set; }
    }
}
