using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Services.Interfaces;
using System.Security.Claims;

namespace SmartFYPHandler.Controllers
{
    [ApiController]
    [Route("api/recommendations")]
    [Authorize]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        /// <summary>
        /// Get personalized recommendations for the current user
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRecommendations(int userId, [FromQuery] int limit = 10)
        {
            try
            {
                // Verify user can access recommendations (own recommendations or admin)
                var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

                if (currentUserRole != "Admin" && currentUserIdClaim != userId.ToString())
                {
                    return Forbid("You can only access your own recommendations");
                }

                var recommendations = await _recommendationService.GetRecommendationsAsync(userId, limit);
                return Ok(new { success = true, data = recommendations });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Track user interaction with a project
        /// </summary>
        [HttpPost("interactions")]
        public async Task<IActionResult> TrackInteraction([FromBody] CreateUserInteractionDto interactionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                var interaction = await _recommendationService.TrackInteractionAsync(userId, interactionDto);
                return Ok(new { success = true, data = interaction });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get user's interaction history
        /// </summary>
        [HttpGet("interactions/my")]
        public async Task<IActionResult> GetMyInteractions()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                var interactions = await _recommendationService.GetUserInteractionsAsync(userId);
                return Ok(new { success = true, data = interactions });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get projects similar to a specific project
        /// </summary>
        [HttpGet("similar/{projectId}")]
        public async Task<IActionResult> GetSimilarProjects(int projectId, [FromQuery] int limit = 5)
        {
            try
            {
                var similarProjects = await _recommendationService.GetSimilarProjectsAsync(projectId, limit);
                return Ok(new { success = true, data = similarProjects });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get trending projects
        /// </summary>
        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingProjects([FromQuery] int limit = 10)
        {
            try
            {
                var trendingProjects = await _recommendationService.GetTrendingProjectsAsync(limit);
                return Ok(new { success = true, data = trendingProjects });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Update user preferences based on interactions
        /// </summary>
        [HttpPost("preferences/update")]
        public async Task<IActionResult> UpdatePreferences()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                await _recommendationService.UpdateUserPreferencesAsync(userId);
                return Ok(new { success = true, message = "Preferences updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}