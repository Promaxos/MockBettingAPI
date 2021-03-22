using MockBettingAPI.API.Validations;

namespace MockBettingAPI.API.Models
{
    [MatchValidation]
    public class MatchToCreateDto
    {
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        /// <summary>
        /// Football = 1 || Basketball = 2
        /// </summary>
        public int Sport { get; set; }
    }
}
