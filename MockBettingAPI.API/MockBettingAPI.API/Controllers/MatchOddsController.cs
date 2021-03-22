using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MockBettingAPI.API.Models;
using MockBettingAPI.Data;
using MockBettingAPI.DataAccess.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockBettingAPI.API.Controllers
{
    [Route("api/matchodds")]
    [ApiController]
    public class MatchOddsController : Controller
    {
        private readonly IMasterManager _db;
        private readonly IMapper _mapper;

        public MatchOddsController(IMasterManager db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("{matchID}/{specifier}", Name = "GetMatchOdds")]
        public async Task<IActionResult> GetMatchOdds(string matchID, string specifier)
        {
            try
            {
                bool valid = int.TryParse(matchID, out int id);

                List<string> specifiers = new List<string>() { "1", "x", "2", "u", "o" };

                if (!valid || !specifiers.Contains(specifier.ToLower()))
                    return BadRequest(ModelState);

                MatchOdds matchOdds = await _db.Matches.GetMatchOdds(id, specifier.ToUpper());

                if (matchOdds == null)
                    return StatusCode(204);
                else
                    return Ok(matchOdds);
            }
            catch (Exception)
            {
                // log exception
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchOdds([FromBody] MatchOddsToCreateDto matchOddsToCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (await _db.Matches.GetMatch(matchOddsToCreate.MatchID) == null)
                    return StatusCode(204);

                if (await _db.Matches.CreateMatchOdds(_mapper.Map<MatchOdds>(matchOddsToCreate)))
                    return Ok();
                else
                    return StatusCode(500);
            }
            catch (Exception ec)
            {
                // log exception
                return StatusCode(500);
            }
        }

        [HttpPut("{matchID}", Name = "UpdateMatchOdds")]
        public async Task<IActionResult> UpdateMatchOdds(string matchID, [FromBody] MatchOddsToCreateDto matchToUpdate)
        {
            try
            {
                bool valid = int.TryParse(matchID, out int id);

                if (!ModelState.IsValid || !valid)
                    return BadRequest(ModelState);

                if (await _db.Matches.GetMatch(matchToUpdate.MatchID) == null)
                    return StatusCode(204);

                if (await _db.Matches.UpdateMatchOdds(_mapper.Map<MatchOdds>(matchToUpdate)))
                    return Ok();
                else
                    return StatusCode(500);
            }
            catch (Exception)
            {
                // log exception
                return StatusCode(500);
            }
        }

        [HttpDelete("{matchID}", Name = "DeleteMatchOdds")]
        public async Task<IActionResult> DeleteMatchOdds(string matchID)
        {
            try
            {
                bool valid = int.TryParse(matchID, out int id);

                if (!valid)
                    return BadRequest(ModelState);

                bool success = await _db.Matches.DeleteMatchOdds(id);

                if (success)
                    return Ok();
                return StatusCode(500);
            }
            catch (Exception)
            {
                // log exception
                return StatusCode(500);
            }
        }
    }
}