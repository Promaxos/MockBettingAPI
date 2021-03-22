using MockBettingAPI.Data;
using System.Threading.Tasks;

namespace MockBettingAPI.DataAccess.Managers.Interfaces
{
    public interface IMatchManager
    {
        /// <summary>
        /// Returns a Match item by its ID.
        /// </summary>
        /// <param name="matchID">The ID of the desired match</param>
        Task<Match> GetMatch(int matchID);
        /// <summary>
        /// Returns the odds of a match.
        /// </summary>
        /// <param name="matchID">The ID of the desired match</param>
        Task<MatchOdds> GetMatchOdds(int matchID, string specifier);

        /// <summary>
        /// Creates a new match and returns it with its ID.
        /// </summary>
        /// <param name="match">The match to create</param>
        /// <returns></returns>
        Task<Match> CreateMatch(Match match);
        /// <summary>
        /// Create odds for a specific match.
        /// </summary>
        /// <param name="matchID">The ID of the desired match</param>
        Task<bool> CreateMatchOdds(MatchOdds odds);

        /// <summary>
        /// Updates an existing match.
        /// </summary>
        /// <param name="match">The updated match</param>
        Task<Match> UpdateMatch(Match match);
        /// <summary>
        /// Updates existing odds for a specific match.
        /// </summary>
        /// <param name="matchID">The ID of the desired match</param>
        Task<bool> UpdateMatchOdds(MatchOdds odds);

        /// <summary>
        /// Deletes an existing match.
        /// </summary>
        /// <param name="matchID">The ID of the desired match</param>
        Task<bool> DeleteMatch(int matchID);
        /// <summary>
        /// Deletes the odds of a match.
        /// </summary>
        /// <param name="matchID">The ID of the desired match</param>
        Task<bool> DeleteMatchOdds(int matchID);
    }
}
