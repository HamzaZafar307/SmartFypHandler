using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFYPHandler.Models.DTOs.Novelty;
using SmartFYPHandler.Services.Interfaces;
using System.Security.Claims;

namespace SmartFYPHandler.Controllers
{
    [ApiController]
    [Route("api/novelty")]
    [Authorize]
    public class NoveltyController : ControllerBase
    {
        private readonly INoveltyService _noveltyService;

        public NoveltyController(INoveltyService noveltyService)
        {
            _noveltyService = noveltyService;
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> Analyze([FromBody] NoveltyAnalyzeRequestDto request, CancellationToken ct)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                if (string.IsNullOrWhiteSpace(request.Title) && string.IsNullOrWhiteSpace(request.Abstract))
                {
                    return BadRequest(new { success = false, message = "Provide Title or Abstract for analysis" });
                }

                var result = await _noveltyService.AnalyzeAsync(request, userId, ct);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("analysis/{id}")]
        public async Task<IActionResult> GetAnalysis(Guid id, CancellationToken ct)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                var result = await _noveltyService.GetAnalysisAsync(id, userId, ct);
                if (result == null)
                {
                    return NotFound(new { success = false, message = "Analysis not found" });
                }

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("reindex")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reindex([FromBody] NoveltyReindexRequestDto request, CancellationToken ct)
        {
            try
            {
                await _noveltyService.ReindexAsync(request, ct);
                return Ok(new { success = true, message = "Reindex triggered" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}

