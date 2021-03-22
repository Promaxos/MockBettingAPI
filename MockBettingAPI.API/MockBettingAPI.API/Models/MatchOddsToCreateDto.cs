using MockBettingAPI.API.Validations;

namespace MockBettingAPI.API.Models
{
    [MatchOddsValidation]
    public class MatchOddsToCreateDto
    {
        public int MatchID { get; set; }
        /// <summary>
        /// 1 = HomeTeam || X = Draw || 2 = AwayTeam || O = Over || U = Under
        /// </summary>
        public string Specifier { get; set; }
        public decimal Odd { get; set; }
    }
}
