using Microsoft.EntityFrameworkCore;
using SmartFYPHandler.Data;
using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Models.DTOs.Authentication;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations
{
    public class ProjectEvaluationService : IProjectEvaluationService
    {
        private readonly ApplicationDbContext _context;

        public ProjectEvaluationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectEvaluationDto>> GetEvaluationsByProjectIdAsync(int projectId)
        {
            var evaluations = await _context.ProjectEvaluations
                .Include(e => e.Project)
                .Include(e => e.Evaluator)
                .Where(e => e.ProjectId == projectId)
                .OrderByDescending(e => e.EvaluationDate)
                .ToListAsync();

            return evaluations.Select(MapToDto);
        }

        public async Task<ProjectEvaluationDto?> GetEvaluationByIdAsync(int id)
        {
            var evaluation = await _context.ProjectEvaluations
                .Include(e => e.Project)
                .Include(e => e.Evaluator)
                .FirstOrDefaultAsync(e => e.Id == id);

            return evaluation == null ? null : MapToDto(evaluation);
        }

        public async Task<ProjectEvaluationDto> CreateEvaluationAsync(int evaluatorId, CreateProjectEvaluationDto createEvaluationDto)
        {
            // Validate project exists
            var project = await _context.FYPProjects.FindAsync(createEvaluationDto.ProjectId);
            if (project == null)
            {
                throw new InvalidOperationException("Project not found.");
            }

            // Validate evaluator is a teacher
            var evaluator = await _context.Users.FindAsync(evaluatorId);
            if (evaluator == null || evaluator.Role != UserRole.Teacher)
            {
                throw new InvalidOperationException("Only teachers can evaluate projects.");
            }

            // Check if evaluation already exists for this evaluator and project
            var existingEvaluation = await _context.ProjectEvaluations
                .FirstOrDefaultAsync(e => e.ProjectId == createEvaluationDto.ProjectId &&
                                        e.EvaluatorId == evaluatorId &&
                                        e.EvaluationType == createEvaluationDto.EvaluationType);

            if (existingEvaluation != null)
            {
                throw new InvalidOperationException($"You have already submitted a {createEvaluationDto.EvaluationType} evaluation for this project.");
            }

            // Calculate overall score
            var overallScore = EvaluationScoreCalculator.CalculateOverallScore(
                createEvaluationDto.TechnicalScore,
                createEvaluationDto.InnovationScore,
                createEvaluationDto.ImplementationScore,
                createEvaluationDto.PresentationScore,
                createEvaluationDto.DocumentationScore);

            var evaluation = new ProjectEvaluation
            {
                ProjectId = createEvaluationDto.ProjectId,
                EvaluatorId = evaluatorId,
                TechnicalScore = createEvaluationDto.TechnicalScore,
                InnovationScore = createEvaluationDto.InnovationScore,
                ImplementationScore = createEvaluationDto.ImplementationScore,
                PresentationScore = createEvaluationDto.PresentationScore,
                DocumentationScore = createEvaluationDto.DocumentationScore,
                OverallScore = overallScore,
                EvaluationType = createEvaluationDto.EvaluationType,
                Comments = createEvaluationDto.Comments,
                Recommendations = createEvaluationDto.Recommendations,
                EvaluationDate = createEvaluationDto.EvaluationDate ?? DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.ProjectEvaluations.Add(evaluation);
            await _context.SaveChangesAsync();

            // Update project performance score
            await UpdateProjectPerformanceScore(createEvaluationDto.ProjectId);

            // Reload with includes
            var createdEvaluation = await _context.ProjectEvaluations
                .Include(e => e.Project)
                .Include(e => e.Evaluator)
                .FirstAsync(e => e.Id == evaluation.Id);

            return MapToDto(createdEvaluation);
        }

        public async Task<ProjectEvaluationDto?> UpdateEvaluationAsync(int id, int evaluatorId, UpdateProjectEvaluationDto updateEvaluationDto)
        {
            var evaluation = await _context.ProjectEvaluations.FindAsync(id);
            if (evaluation == null) return null;

            // Check if the evaluator owns this evaluation
            if (evaluation.EvaluatorId != evaluatorId)
            {
                throw new UnauthorizedAccessException("You can only update your own evaluations.");
            }

            // Update fields if provided
            if (updateEvaluationDto.TechnicalScore.HasValue)
                evaluation.TechnicalScore = updateEvaluationDto.TechnicalScore.Value;

            if (updateEvaluationDto.InnovationScore.HasValue)
                evaluation.InnovationScore = updateEvaluationDto.InnovationScore.Value;

            if (updateEvaluationDto.ImplementationScore.HasValue)
                evaluation.ImplementationScore = updateEvaluationDto.ImplementationScore.Value;

            if (updateEvaluationDto.PresentationScore.HasValue)
                evaluation.PresentationScore = updateEvaluationDto.PresentationScore.Value;

            if (updateEvaluationDto.DocumentationScore.HasValue)
                evaluation.DocumentationScore = updateEvaluationDto.DocumentationScore.Value;

            if (updateEvaluationDto.EvaluationType.HasValue)
                evaluation.EvaluationType = updateEvaluationDto.EvaluationType.Value;

            if (updateEvaluationDto.Comments != null)
                evaluation.Comments = updateEvaluationDto.Comments;

            if (updateEvaluationDto.Recommendations != null)
                evaluation.Recommendations = updateEvaluationDto.Recommendations;

            if (updateEvaluationDto.EvaluationDate.HasValue)
                evaluation.EvaluationDate = updateEvaluationDto.EvaluationDate.Value;

            // Recalculate overall score
            evaluation.OverallScore = EvaluationScoreCalculator.CalculateOverallScore(
                evaluation.TechnicalScore,
                evaluation.InnovationScore,
                evaluation.ImplementationScore,
                evaluation.PresentationScore,
                evaluation.DocumentationScore);

            evaluation.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Update project performance score
            await UpdateProjectPerformanceScore(evaluation.ProjectId);

            // Reload with includes
            var updatedEvaluation = await _context.ProjectEvaluations
                .Include(e => e.Project)
                .Include(e => e.Evaluator)
                .FirstAsync(e => e.Id == evaluation.Id);

            return MapToDto(updatedEvaluation);
        }

        public async Task<bool> DeleteEvaluationAsync(int id, int evaluatorId)
        {
            var evaluation = await _context.ProjectEvaluations.FindAsync(id);
            if (evaluation == null) return false;

            // Check if the evaluator owns this evaluation
            if (evaluation.EvaluatorId != evaluatorId)
            {
                throw new UnauthorizedAccessException("You can only delete your own evaluations.");
            }

            var projectId = evaluation.ProjectId;
            _context.ProjectEvaluations.Remove(evaluation);
            await _context.SaveChangesAsync();

            // Update project performance score
            await UpdateProjectPerformanceScore(projectId);

            return true;
        }

        public async Task<IEnumerable<ProjectEvaluationDto>> GetEvaluationsByEvaluatorAsync(int evaluatorId)
        {
            var evaluations = await _context.ProjectEvaluations
                .Include(e => e.Project)
                .Include(e => e.Evaluator)
                .Where(e => e.EvaluatorId == evaluatorId)
                .OrderByDescending(e => e.EvaluationDate)
                .ToListAsync();

            return evaluations.Select(MapToDto);
        }

        public async Task<bool> CanEvaluateProjectAsync(int evaluatorId, int projectId)
        {
            var evaluator = await _context.Users.FindAsync(evaluatorId);
            if (evaluator == null || evaluator.Role != UserRole.Teacher)
            {
                return false;
            }

            var project = await _context.FYPProjects.FindAsync(projectId);
            return project != null;
        }

        public async Task<decimal> CalculateProjectPerformanceScoreAsync(int projectId)
        {
            var evaluations = await _context.ProjectEvaluations
                .Where(e => e.ProjectId == projectId)
                .ToListAsync();

            if (!evaluations.Any())
                return 0;

            // Calculate average of all evaluation scores, normalized to 0-100 scale
            var averageScore = evaluations.Average(e => e.OverallScore);
            return Math.Round(averageScore * 10, 2); // Convert 0-10 scale to 0-100 scale
        }

        public async Task UpdateProjectGradesAsync()
        {
            var projects = await _context.FYPProjects.ToListAsync();

            foreach (var project in projects)
            {
                var performanceScore = await CalculateProjectPerformanceScoreAsync(project.Id);
                project.PerformanceScore = performanceScore;

                // Convert performance score back to 0-10 scale for grade calculation
                var gradeScore = performanceScore / 10;
                project.FinalGrade = EvaluationScoreCalculator.GetGradeFromScore(gradeScore);
                project.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        private async Task UpdateProjectPerformanceScore(int projectId)
        {
            var project = await _context.FYPProjects.FindAsync(projectId);
            if (project == null) return;

            var performanceScore = await CalculateProjectPerformanceScoreAsync(projectId);
            project.PerformanceScore = performanceScore;

            // Convert performance score back to 0-10 scale for grade calculation
            var gradeScore = performanceScore / 10;
            project.FinalGrade = EvaluationScoreCalculator.GetGradeFromScore(gradeScore);
            project.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        private static ProjectEvaluationDto MapToDto(ProjectEvaluation evaluation)
        {
            return new ProjectEvaluationDto
            {
                Id = evaluation.Id,
                ProjectId = evaluation.ProjectId,
                ProjectTitle = evaluation.Project?.Title ?? "",
                EvaluatorId = evaluation.EvaluatorId,
                EvaluatorName = $"{evaluation.Evaluator?.FirstName} {evaluation.Evaluator?.LastName}".Trim(),
                TechnicalScore = evaluation.TechnicalScore,
                InnovationScore = evaluation.InnovationScore,
                ImplementationScore = evaluation.ImplementationScore,
                PresentationScore = evaluation.PresentationScore,
                DocumentationScore = evaluation.DocumentationScore,
                OverallScore = evaluation.OverallScore,
                EvaluationType = evaluation.EvaluationType.ToString(),
                Comments = evaluation.Comments,
                Recommendations = evaluation.Recommendations,
                EvaluationDate = evaluation.EvaluationDate,
                CreatedAt = evaluation.CreatedAt,
                UpdatedAt = evaluation.UpdatedAt
            };
        }
    }
}