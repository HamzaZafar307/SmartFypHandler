using Microsoft.EntityFrameworkCore;
using SmartFYPHandler.Data;
using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Models.DTOs.Authentication;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations
{
    public class FYPProjectService : IFYPProjectService
    {
        private readonly ApplicationDbContext _context;

        public FYPProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<FYPProjectDto>> GetProjectsAsync(FYPProjectSearchDto searchDto)
        {
            var query = _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchDto.Title))
            {
                query = query.Where(p => p.Title.Contains(searchDto.Title));
            }

            if (!string.IsNullOrEmpty(searchDto.Category))
            {
                query = query.Where(p => p.Category == searchDto.Category);
            }

            if (searchDto.DepartmentId.HasValue)
            {
                query = query.Where(p => p.DepartmentId == searchDto.DepartmentId.Value);
            }

            if (searchDto.SupervisorId.HasValue)
            {
                query = query.Where(p => p.SupervisorId == searchDto.SupervisorId.Value);
            }

            if (searchDto.Year.HasValue)
            {
                query = query.Where(p => p.Year == searchDto.Year.Value);
            }

            if (!string.IsNullOrEmpty(searchDto.Semester))
            {
                query = query.Where(p => p.Semester == searchDto.Semester);
            }

            if (searchDto.Status.HasValue)
            {
                query = query.Where(p => p.Status == searchDto.Status.Value);
            }

            if (!string.IsNullOrEmpty(searchDto.DifficultyLevel))
            {
                query = query.Where(p => p.DifficultyLevel == searchDto.DifficultyLevel);
            }

            var totalCount = await query.CountAsync();

            var projects = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((searchDto.Page - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
                .ToListAsync();

            var projectDtos = projects.Select(MapToDto).ToList();

            var totalPages = (int)Math.Ceiling((double)totalCount / searchDto.PageSize);

            return new PagedResult<FYPProjectDto>
            {
                Items = projectDtos,
                TotalCount = totalCount,
                Page = searchDto.Page,
                PageSize = searchDto.PageSize,
                TotalPages = totalPages,
                HasNextPage = searchDto.Page < totalPages,
                HasPreviousPage = searchDto.Page > 1
            };
        }

        public async Task<FYPProjectDto?> GetProjectByIdAsync(int id)
        {
            var project = await _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            return project == null ? null : MapToDto(project);
        }

        public async Task<FYPProjectDto> CreateProjectAsync(CreateFYPProjectDto createProjectDto)
        {
            // Validate department exists
            var department = await _context.Departments.FindAsync(createProjectDto.DepartmentId);
            if (department == null)
            {
                throw new InvalidOperationException("Department not found.");
            }

            // Validate supervisor exists and is a teacher
            var supervisor = await _context.Users.FindAsync(createProjectDto.SupervisorId);
            if (supervisor == null || supervisor.Role != UserRole.Teacher)
            {
                throw new InvalidOperationException("Supervisor must be a teacher.");
            }

            // Validate students exist
            if (createProjectDto.StudentIds.Any())
            {
                var students = await _context.Users
                    .Where(u => createProjectDto.StudentIds.Contains(u.Id) && u.Role == UserRole.Student)
                    .ToListAsync();

                if (students.Count != createProjectDto.StudentIds.Count)
                {
                    throw new InvalidOperationException("One or more students not found or not valid students.");
                }
            }

            var project = new FYPProject
            {
                Title = createProjectDto.Title,
                Description = createProjectDto.Description,
                Year = createProjectDto.Year,
                Semester = createProjectDto.Semester,
                Category = createProjectDto.Category,
                Status = ProjectStatus.Proposed,
                DepartmentId = createProjectDto.DepartmentId,
                SupervisorId = createProjectDto.SupervisorId,
                DifficultyLevel = createProjectDto.DifficultyLevel,
                PerformanceScore = 0,
                Citations = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.FYPProjects.Add(project);
            await _context.SaveChangesAsync();

            // Add project members
            foreach (var studentId in createProjectDto.StudentIds)
            {
                var projectMember = new ProjectMember
                {
                    FYPProjectId = project.Id,
                    UserId = studentId,
                    Role = MemberRole.Member,
                    JoinedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.ProjectMembers.Add(projectMember);
            }

            await _context.SaveChangesAsync();

            // Reload the project with includes
            var createdProject = await _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .FirstAsync(p => p.Id == project.Id);

            return MapToDto(createdProject);
        }

        public async Task<FYPProjectDto?> UpdateProjectAsync(int id, UpdateFYPProjectDto updateProjectDto)
        {
            var project = await _context.FYPProjects.FindAsync(id);
            if (project == null) return null;

            // Validate department if provided
            if (updateProjectDto.DepartmentId.HasValue)
            {
                var department = await _context.Departments.FindAsync(updateProjectDto.DepartmentId.Value);
                if (department == null)
                {
                    throw new InvalidOperationException("Department not found.");
                }
            }

            // Validate supervisor if provided
            if (updateProjectDto.SupervisorId.HasValue)
            {
                var supervisor = await _context.Users.FindAsync(updateProjectDto.SupervisorId.Value);
                if (supervisor == null || supervisor.Role != UserRole.Teacher)
                {
                    throw new InvalidOperationException("Supervisor must be a teacher.");
                }
            }

            // Update fields if provided
            if (!string.IsNullOrEmpty(updateProjectDto.Title))
                project.Title = updateProjectDto.Title;

            if (!string.IsNullOrEmpty(updateProjectDto.Description))
                project.Description = updateProjectDto.Description;

            if (updateProjectDto.Year.HasValue)
                project.Year = updateProjectDto.Year.Value;

            if (!string.IsNullOrEmpty(updateProjectDto.Semester))
                project.Semester = updateProjectDto.Semester;

            if (!string.IsNullOrEmpty(updateProjectDto.Category))
                project.Category = updateProjectDto.Category;

            if (updateProjectDto.Status.HasValue)
                project.Status = updateProjectDto.Status.Value;

            if (updateProjectDto.DepartmentId.HasValue)
                project.DepartmentId = updateProjectDto.DepartmentId.Value;

            if (updateProjectDto.SupervisorId.HasValue)
                project.SupervisorId = updateProjectDto.SupervisorId.Value;

            if (!string.IsNullOrEmpty(updateProjectDto.DifficultyLevel))
                project.DifficultyLevel = updateProjectDto.DifficultyLevel;

            if (updateProjectDto.PerformanceScore.HasValue)
                project.PerformanceScore = updateProjectDto.PerformanceScore.Value;

            if (!string.IsNullOrEmpty(updateProjectDto.FinalGrade))
                project.FinalGrade = updateProjectDto.FinalGrade;

            if (updateProjectDto.Citations.HasValue)
                project.Citations = updateProjectDto.Citations.Value;

            project.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Reload the project with includes
            var updatedProject = await _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .FirstAsync(p => p.Id == project.Id);

            return MapToDto(updatedProject);
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.FYPProjects.FindAsync(id);
            if (project == null) return false;

            _context.FYPProjects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<string>> GetProjectCategoriesAsync()
        {
            return await _context.FYPProjects
                .Select(p => p.Category)
                .Distinct()
                .Where(c => !string.IsNullOrEmpty(c))
                .OrderBy(c => c)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetProjectYearsAsync()
        {
            return await _context.FYPProjects
                .Select(p => p.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToListAsync();
        }

        public async Task<bool> ProjectExistsAsync(int id)
        {
            return await _context.FYPProjects.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<FYPProjectDto>> GetProjectsBySupervisorAsync(int supervisorId)
        {
            var projects = await _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .Include(p => p.ProjectMembers)
                    .ThenInclude(pm => pm.User)
                .Where(p => p.SupervisorId == supervisorId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return projects.Select(MapToDto);
        }

        private static FYPProjectDto MapToDto(FYPProject project)
        {
            return new FYPProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Year = project.Year,
                Semester = project.Semester,
                Category = project.Category,
                Status = project.Status.ToString(),
                DepartmentId = project.DepartmentId,
                DepartmentName = project.Department?.Name ?? "",
                SupervisorId = project.SupervisorId,
                SupervisorName = $"{project.Supervisor?.FirstName} {project.Supervisor?.LastName}".Trim(),
                DifficultyLevel = project.DifficultyLevel,
                PerformanceScore = project.PerformanceScore,
                FinalGrade = project.FinalGrade,
                DepartmentRank = project.DepartmentRank,
                OverallRank = project.OverallRank,
                Citations = project.Citations,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
                ProjectMembers = project.ProjectMembers?.Select(pm => new ProjectMemberDto
                {
                    Id = pm.Id,
                    UserId = pm.UserId,
                    UserName = $"{pm.User?.FirstName} {pm.User?.LastName}".Trim(),
                    Role = pm.Role.ToString(),
                    JoinedAt = pm.JoinedAt
                }).ToList() ?? new List<ProjectMemberDto>()
            };
        }
    }
}