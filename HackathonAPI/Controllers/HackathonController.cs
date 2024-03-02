using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HackathonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackathonController : ControllerBase
    {
        private readonly IHackathonRepository _hackathonRepository;

        public HackathonController(IHackathonRepository hackathonRepository)
        {
            _hackathonRepository = hackathonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHackathons()
        {
            try
            {
                var hackathons = await _hackathonRepository.GetAllHackathons();
                return Ok(hackathons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHackathonById(int id)
        {
            try
            {
                var hackathon = await _hackathonRepository.GetHackathonById(id);
                if (hackathon == null)
                {
                    return NotFound($"Hackathon with id {id} not found.");
                }
                return Ok(hackathon);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddHackathon([FromBody] Hackathon hackathon)
        {
            try
            {
                var result = await _hackathonRepository.AddHackathon(hackathon);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHackathon(int id, [FromBody] Hackathon hackathon)
        {
            try
            {
                hackathon.HackathonID = id; // Ensure ID is set to the correct value
                var result = await _hackathonRepository.UpdateHackathon(hackathon);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHackathon(int id)
        {
            try
            {
                var result = await _hackathonRepository.DeleteHackathon(id);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

            [HttpPost("RegisterTeam")]
            public async Task<IActionResult> RegisterTeam([FromBody] TeamRegisteration teamRegistration)
            {
                try
                {
                    var result = await _hackathonRepository.RegisterInHackathon(teamRegistration);

                    if (result.IsSuccess)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }

 }


