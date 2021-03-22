using Microsoft.EntityFrameworkCore;
using MockBettingAPI.Data;
using MockBettingAPI.DataAccess.Managers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockBettingAPI.DataAccess.Managers
{
    public class MatchManager : IMatchManager
    {
        private readonly Context _db;

        public MatchManager(Context db)
        {
            _db = db;
        }

        // CREATE
        public async Task<Match> CreateMatch(Match match)
        {
            await _db.Matches.AddAsync(match);
            await _db.SaveChangesAsync();

            return match;
        }

        public async Task<bool> CreateMatchOdds(MatchOdds odds)
        {
            if (await GetMatchOdds(odds.MatchID, odds.Specifier) != null)
                await UpdateMatchOdds(odds);
            else
            {
                await _db.MatchOdds.AddAsync(odds);
                await _db.SaveChangesAsync();
            }

            return true;
        }

        // READ
        public async Task<Match> GetMatch(int matchID)
        {
            return await _db.Matches.FindAsync(matchID);
        }

        public async Task<MatchOdds> GetMatchOdds(int matchID, string specifier)
        {
            return await _db.MatchOdds.Where(x => x.MatchID == matchID && x.Specifier == specifier).FirstOrDefaultAsync();
        }

        // UPDATE
        public async Task<Match> UpdateMatch(Match match)
        {
            Match oldMatch = await _db.Matches.FindAsync(match.ID);

            if (oldMatch == null)
                return null;

            _db.Entry(oldMatch).CurrentValues.SetValues(match);
            await _db.SaveChangesAsync();

            return match;
        }

        public async Task<bool> UpdateMatchOdds(MatchOdds odds)
        {
            MatchOdds oldOdds = await _db.MatchOdds.Where(x => x.MatchID == odds.MatchID && x.Specifier == odds.Specifier).FirstOrDefaultAsync();

            if (oldOdds == null)
                await CreateMatchOdds(odds);
            else
            {
                oldOdds.Odd = odds.Odd;
                _db.Entry(oldOdds).CurrentValues.SetValues(oldOdds);
            }

            await _db.SaveChangesAsync();
            return true;
        }

        // DELETE
        public async Task<bool> DeleteMatch(int matchID)
        {
            Match oldMatch = await _db.Matches.FindAsync(matchID);

            if (oldMatch == null)
                return false;

            _db.Matches.Remove(oldMatch);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteMatchOdds(int matchID)
        {
            List<MatchOdds> matchOddsToDelete = await _db.MatchOdds.Where(x => x.MatchID == matchID).ToListAsync();

            foreach (MatchOdds odds in matchOddsToDelete)
            {
                _db.MatchOdds.Remove(odds);
            }

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
