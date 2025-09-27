using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Services.Interfaces;
using System.Security.Claims;

namespace SmartFYPHandler.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class FYPProjectController : ControllerBase
    {
        private readonly IFYPProjectService _fypProjectService;

        public FYPProjectController(IFYPProjectService fypProjectService)
        {
            _fypProjectService = fypProjectService;
        }

        /// <summary>
        /// Search projects with filters and pagination
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchProjects([FromQuery] FYPProjectSearchDto searchDto)
        {
            try
            {
                var result = await _fypProjectService.GetProjectsAsync(searchDto);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get project by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                var project = await _fypProjectService.GetProjectByIdAsync(id);
                if (project == null)
                {
                    return NotFound(new { success = false, message = "Project not found" });
                }

                return Ok(new { success = true, data = project });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Create a new project (Teachers and Admins only)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> CreateProject([FromBody] CreateFYPProjectDto createProjectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var project = await _fypProjectService.CreateProjectAsync(createProjectDto);
                return CreatedAtAction(nameof(GetProjectById), new { id = project.Id },
                    new { success = true, data = project });
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
        /// Update a project (Teachers and Admins only)
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateFYPProjectDto updateProjectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var project = await _fypProjectService.UpdateProjectAsync(id, updateProjectDto);
                if (project == null)
                {
                    return NotFound(new { success = false, message = "Project not found" });
                }

                return Ok(new { success = true, data = project });
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
        /// Delete a project (Admins only)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var result = await _fypProjectService.DeleteProjectAsync(id);
                if (!result)
                {
                    return NotFound(new { success = false, message = "Project not found" });
                }

                return Ok(new { success = true, message = "Project deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get all project categories
        /// </summary>
        [HttpGet("categories")]
        public async Task<IActionResult> GetProjectCategories()
        {
            try
            {
                var categories = await _fypProjectService.GetProjectCategoriesAsync();
                return Ok(new { success = true, data = categories });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get all project years
        /// </summary>
        [HttpGet("years")]
        public async Task<IActionResult> GetProjectYears()
        {
            try
            {
                var years = await _fypProjectService.GetProjectYearsAsync();
                return Ok(new { success = true, data = years });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Get projects by supervisor (Teachers can see their own projects)
        /// </summary>
        [HttpGet("teacher/{teacherId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetProjectsBySupervisor(int teacherId)
        {
            try
            {
                // Check if user is accessing their own projects or is an admin
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                if (userRole != "Admin" && userIdClaim != teacherId.ToString())
                {
                    return Forbid("You can only access your own projects");
                }

                var projects = await _fypProjectService.GetProjectsBySupervisorAsync(teacherId);
                return Ok(new { success = true, data = projects });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}