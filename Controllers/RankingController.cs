using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Controllers
{
    [ApiController]
    [Route("api/rankings")]
    public class RankingController : ControllerBase
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        /// <summary>
        /// Get department rankings
        /// </summary>
        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartmentRankings([FromQuery] int departmentId, [FromQuery] int? year = null, [FromQuery] string? semester = null)
        {
            try
            {
                var rankings = await _rankingService.GetDepartmentRankingsAsync(departmentId, year, semester);
                return Ok(new { success = true, data = rankings });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get overall rankings
        /// </summary>
        [HttpGet("overall")]
        public async Task<IActionResult> GetOverallRankings([FromQuery] int? year = null, [FromQuery] string? semester = null, [FromQuery] int? top = null)
        {
            try
            {
                var rankings = await _rankingService.GetOverallRankingsAsync(year, semester, top);
                return Ok(new { success = true, data = rankings });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get ranking statistics
        /// </summary>
        [HttpGet("stats")]
        public async Task<IActionResult> GetRankingStats()
        {
            try
            {
                var stats = await _rankingService.GetRankingStatsAsync();
                return Ok(new { success = true, data = stats });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get top projects by department
        /// </summary>
        [HttpGet("top/department/{departmentId}")]
        public async Task<IActionResult> GetTopProjectsByDepartment(int departmentId, [FromQuery] int top = 10)
        {
            try
            {
                var rankings = await _rankingService.GetTopProjectsByDepartmentAsync(departmentId, top);
                return Ok(new { success = true, data = rankings });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get top projects overall
        /// </summary>
        [HttpGet("top/overall")]
        public async Task<IActionResult> GetTopProjectsOverall([FromQuery] int top = 10)
        {
            try
            {
                var rankings = await _rankingService.GetTopProjectsOverallAsync(top);
                return Ok(new { success = true, data = rankings });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Update rankings (Admin only)
        /// </summary>
        [HttpPost("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRankings()
        {
            try
            {
                await _rankingService.UpdateRankingsAsync();
                return Ok(new { success = true, message = "Rankings updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}