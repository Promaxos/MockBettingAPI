using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MockBettingAPI.API.Models;
using MockBettingAPI.Data;
using MockBettingAPI.DataAccess.Managers.Interfaces;
using System;
using System.Threading.Tasks;

namespace MockBettingAPI.API.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchesController : Controller
    {
        private readonly IMasterManager _db;
        private readonly IMapper _mapper;

        public MatchesController(IMasterManager db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("{matchID}", Name = "GetMatch")]
        public async Task<IActionResult> GetMatch(string matchID)
        {
            try
            {
                bool valid = int.TryParse(matchID, out int id);

                if (!valid)
                    return BadRequest(ModelState);

                Match match = await _db.Matches.GetMatch(id);

                if (match == null)
                    return StatusCode(204);
                else                    
                    return Ok(_mapper.Map<MatchToReturnDto>(match));
            }
            catch (Exception)
            {
                // log exception
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] MatchToCreateDto matchToCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Match newMatch = new Match()
                {
                    Description = matchToCreate.TeamA + "-" + matchToCreate.TeamB,
                    MatchDate = Convert.ToDateTime(matchToCreate.MatchDate),
                    MatchTime = TimeSpan.Parse(matchToCreate.MatchTime),
                    TeamA = matchToCreate.TeamA,
                    TeamB = matchToCreate.TeamB,
                    Sport = (Sport)matchToCreate.Sport
                };

                newMatch = await _db.Matches.CreateMatch(newMatch);

                return Created(
                    $"api/matches/GetMatch/{newMatch.ID}", 
                    _mapper.Map<MatchToReturnDto>(newMatch));
            }
            catch (Exception)
            {
                // log exception
                return StatusCode(500);
            }
        }

        [HttpPut("{matchID}", Name = "UpdateMatch")]
        public async Task<IActionResult> UpdateMatch(string matchID, [FromBody] MatchToCreateDto matchToUpdate)
        {
            try
            {
                bool valid = int.TryParse(matchID, out int id);

                if (!ModelState.IsValid || !valid)
                    return BadRequest(ModelState);

                Match newMatch = new Match()
                {
                    ID = id,
                    Description = matchToUpdate.TeamA + "-" + matchToUpdate.TeamB,
                    MatchDate = Convert.ToDateTime(matchToUpdate.MatchDate),
                    MatchTime = TimeSpan.Parse(matchToUpdate.MatchTime),
                    TeamA = matchToUpdate.TeamA,
                    TeamB = matchToUpdate.TeamB,
                    Sport = (Sport)matchToUpdate.Sport
                };

                newMatch = await _db.Matches.UpdateMatch(newMatch);

                return Created(
                    $"api/matches/UpdateMatch/{newMatch.ID}",
                    _mapper.Map<MatchToReturnDto>(newMatch));
            }
            catch (Exception)
            {
                // log exception
                return StatusCode(500);
            }
        }

        [HttpDelete("{matchID}", Name = "DeleteMatch")]
        public async Task<IActionResult> DeleteMatch(string matchID)
        {
            try
            {
                bool valid = int.TryParse(matchID, out int id);

                if (!valid)
                    return BadRequest(ModelState);

                bool success = await _db.Matches.DeleteMatch(id);

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
