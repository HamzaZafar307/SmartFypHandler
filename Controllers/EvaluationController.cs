using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Services.Interfaces;
using System.Security.Claims;

namespace SmartFYPHandler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Teacher,Admin")]
    public class EvaluationController : ControllerBase
    {
        private readonly IProjectEvaluationService _evaluationService;

        public EvaluationController(IProjectEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        /// <summary>
        /// Get evaluations for a specific project
        /// </summary>
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetEvaluationsByProject(int projectId)
        {
            try
            {
                var evaluations = await _evaluationService.GetEvaluationsByProjectIdAsync(projectId);
                return Ok(new { success = true, data = evaluations });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get evaluation by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvaluationById(int id)
        {
            try
            {
                var evaluation = await _evaluationService.GetEvaluationByIdAsync(id);
                if (evaluation == null)
                {
                    return NotFound(new { success = false, message = "Evaluation not found" });
                }

                return Ok(new { success = true, data = evaluation });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Create a new evaluation
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateEvaluation([FromBody] CreateProjectEvaluationDto createEvaluationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int evaluatorId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                var evaluation = await _evaluationService.CreateEvaluationAsync(evaluatorId, createEvaluationDto);
                return CreatedAtAction(nameof(GetEvaluationById), new { id = evaluation.Id },
                    new { success = true, data = evaluation });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Update an evaluation
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvaluation(int id, [FromBody] UpdateProjectEvaluationDto updateEvaluationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int evaluatorId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                var evaluation = await _evaluationService.UpdateEvaluationAsync(id, evaluatorId, updateEvaluationDto);
                if (evaluation == null)
                {
                    return NotFound(new { success = false, message = "Evaluation not found" });
                }

                return Ok(new { success = true, data = evaluation });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Delete an evaluation
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluation(int id)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int evaluatorId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                var result = await _evaluationService.DeleteEvaluationAsync(id, evaluatorId);
                if (!result)
                {
                    return NotFound(new { success = false, message = "Evaluation not found" });
                }

                return Ok(new { success = true, message = "Evaluation deleted successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get evaluations by current evaluator
        /// </summary>
        [HttpGet("my-evaluations")]
        public async Task<IActionResult> GetMyEvaluations()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int evaluatorId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                var evaluations = await _evaluationService.GetEvaluationsByEvaluatorAsync(evaluatorId);
                return Ok(new { success = true, data = evaluations });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Check if current user can evaluate a project
        /// </summary>
        [HttpGet("can-evaluate/{projectId}")]
        public async Task<IActionResult> CanEvaluateProject(int projectId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null || !int.TryParse(userIdClaim, out int evaluatorId))
                {
                    return Unauthorized(new { success = false, message = "Invalid token" });
                }

                var canEvaluate = await _evaluationService.CanEvaluateProjectAsync(evaluatorId, projectId);
                return Ok(new { success = true, canEvaluate });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Update all project grades based on evaluations (Admin only)
        /// </summary>
        [HttpPost("update-grades")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProjectGrades()
        {
            try
            {
                await _evaluationService.UpdateProjectGradesAsync();
                return Ok(new { success = true, message = "Project grades updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}