using Microsoft.EntityFrameworkCore;
using SmartFYPHandler.Data;
using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;
using System.Text.Json;

namespace SmartFYPHandler.Services.Implementations
{
    public class RecommendationService : IRecommendationService
    {
        private readonly ApplicationDbContext _context;

        public RecommendationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecommendationDto>> GetRecommendationsAsync(int userId, int limit = 10)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Get user preferences and interactions
            var userPreferences = await _context.UserPreferences.FirstOrDefaultAsync(up => up.UserId == userId);
            var userInteractions = await _context.UserInteractions
                .Where(ui => ui.UserId == userId)
                .ToListAsync();

            // Get available projects (completed projects for reference)
            var availableProjects = await _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .Where(p => p.Status == ProjectStatus.Completed)
                .ToListAsync();

            var recommendations = new List<(FYPProject project, decimal score, string reason)>();

            foreach (var project in availableProjects)
            {
                var score = CalculateRecommendationScore(project, user, userPreferences, userInteractions);
                var reason = GenerateRecommendationReason(project, user, userPreferences, userInteractions);
                recommendations.Add((project, score, reason));
            }

            var topRecommendations = recommendations
                .OrderByDescending(r => r.score)
                .Take(limit)
                .Select(r => new RecommendationDto
                {
                    ProjectId = r.project.Id,
                    ProjectTitle = r.project.Title,
                    ProjectDescription = r.project.Description,
                    Category = r.project.Category,
                    DepartmentName = r.project.Department?.Name ?? "",
                    SupervisorName = $"{r.project.Supervisor?.FirstName} {r.project.Supervisor?.LastName}".Trim(),
                    PerformanceScore = r.project.PerformanceScore,
                    DifficultyLevel = r.project.DifficultyLevel,
                    RecommendationScore = r.score,
                    RecommendationReason = r.reason,
                    Year = r.project.Year,
                    Semester = r.project.Semester
                });

            return topRecommendations;
        }

        public async Task<UserInteractionDto> TrackInteractionAsync(int userId, CreateUserInteractionDto interactionDto)
        {
            // Validate project exists
            var project = await _context.FYPProjects.FindAsync(interactionDto.ProjectId);
            if (project == null)
            {
                throw new InvalidOperationException("Project not found.");
            }

            // Parse interaction type
            if (!Enum.TryParse<InteractionType>(interactionDto.InteractionType, true, out var interactionType))
            {
                throw new InvalidOperationException("Invalid interaction type.");
            }

            var interaction = new UserInteraction
            {
                UserId = userId,
                ProjectId = interactionDto.ProjectId,
                InteractionType = interactionType,
                Rating = interactionDto.Rating,
                Timestamp = DateTime.UtcNow
            };

            _context.UserInteractions.Add(interaction);
            await _context.SaveChangesAsync();

            // Update user preferences based on this interaction
            await UpdateUserPreferencesAsync(userId);

            return new UserInteractionDto
            {
                ProjectId = interaction.ProjectId,
                InteractionType = interaction.InteractionType.ToString(),
                Rating = interaction.Rating,
                Timestamp = interaction.Timestamp
            };
        }

        public async Task<IEnumerable<UserInteractionDto>> GetUserInteractionsAsync(int userId)
        {
            var interactions = await _context.UserInteractions
                .Where(ui => ui.UserId == userId)
                .OrderByDescending(ui => ui.Timestamp)
                .ToListAsync();

            return interactions.Select(i => new UserInteractionDto
            {
                ProjectId = i.ProjectId,
                InteractionType = i.InteractionType.ToString(),
                Rating = i.Rating,
                Timestamp = i.Timestamp
            });
        }

        public async Task UpdateUserPreferencesAsync(int userId)
        {
            var userInteractions = await _context.UserInteractions
                .Include(ui => ui.Project)
                .Where(ui => ui.UserId == userId)
                .ToListAsync();

            if (!userInteractions.Any())
                return;

            // Calculate preferred categories based on interactions
            var categoryInteractions = userInteractions
                .Where(ui => ui.Project != null)
                .GroupBy(ui => ui.Project!.Category)
                .ToDictionary(g => g.Key, g => g.Count());

            var preferredCategories = categoryInteractions
                .OrderByDescending(kvp => kvp.Value)
                .Take(3)
                .Select(kvp => kvp.Key)
                .ToList();

            // Calculate engagement level
            var totalInteractions = userInteractions.Count;
            var engagementLevel = totalInteractions switch
            {
                > 20 => EngagementLevel.High,
                > 5 => EngagementLevel.Medium,
                _ => EngagementLevel.Low
            };

            // Calculate average rating
            var ratingsGiven = userInteractions.Where(ui => ui.Rating.HasValue).ToList();
            var averageRating = ratingsGiven.Any() ? (decimal)ratingsGiven.Average(ui => ui.Rating!.Value) : 0;

            // Update or create user preferences
            var userPreferences = await _context.UserPreferences.FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPreferences == null)
            {
                userPreferences = new UserPreference
                {
                    UserId = userId,
                    PreferredCategories = JsonSerializer.Serialize(preferredCategories),
                    EngagementLevel = engagementLevel,
                    AverageRating = averageRating,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.UserPreferences.Add(userPreferences);
            }
            else
            {
                userPreferences.PreferredCategories = JsonSerializer.Serialize(preferredCategories);
                userPreferences.EngagementLevel = engagementLevel;
                userPreferences.AverageRating = averageRating;
                userPreferences.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecommendationDto>> GetSimilarProjectsAsync(int projectId, int limit = 5)
        {
            var project = await _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                throw new InvalidOperationException("Project not found.");
            }

            // Find similar projects based on category, department, and difficulty
            var similarProjects = await _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .Where(p => p.Id != projectId && p.Status == ProjectStatus.Completed)
                .Where(p => p.Category == project.Category || p.DepartmentId == project.DepartmentId)
                .OrderByDescending(p => p.PerformanceScore)
                .Take(limit)
                .ToListAsync();

            return similarProjects.Select(p => new RecommendationDto
            {
                ProjectId = p.Id,
                ProjectTitle = p.Title,
                ProjectDescription = p.Description,
                Category = p.Category,
                DepartmentName = p.Department?.Name ?? "",
                SupervisorName = $"{p.Supervisor?.FirstName} {p.Supervisor?.LastName}".Trim(),
                PerformanceScore = p.PerformanceScore,
                DifficultyLevel = p.DifficultyLevel,
                RecommendationScore = CalculateSimilarityScore(project, p),
                RecommendationReason = $"Similar to {project.Title} - same {(p.Category == project.Category ? "category" : "department")}",
                Year = p.Year,
                Semester = p.Semester
            });
        }

        public async Task<IEnumerable<RecommendationDto>> GetTrendingProjectsAsync(int limit = 10)
        {
            // Get projects with high interaction rates in the last 30 days
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

            var trendingProjects = await _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .Include(p => p.UserInteractions)
                .Where(p => p.Status == ProjectStatus.Completed)
                .Select(p => new
                {
                    Project = p,
                    RecentInteractions = p.UserInteractions.Count(ui => ui.Timestamp >= thirtyDaysAgo),
                    TotalInteractions = p.UserInteractions.Count()
                })
                .OrderByDescending(x => x.RecentInteractions)
                .ThenByDescending(x => x.Project.PerformanceScore)
                .Take(limit)
                .ToListAsync();

            return trendingProjects.Select(tp => new RecommendationDto
            {
                ProjectId = tp.Project.Id,
                ProjectTitle = tp.Project.Title,
                ProjectDescription = tp.Project.Description,
                Category = tp.Project.Category,
                DepartmentName = tp.Project.Department?.Name ?? "",
                SupervisorName = $"{tp.Project.Supervisor?.FirstName} {tp.Project.Supervisor?.LastName}".Trim(),
                PerformanceScore = tp.Project.PerformanceScore,
                DifficultyLevel = tp.Project.DifficultyLevel,
                RecommendationScore = tp.RecentInteractions,
                RecommendationReason = $"Trending project with {tp.RecentInteractions} recent views",
                Year = tp.Project.Year,
                Semester = tp.Project.Semester
            });
        }

        private static decimal CalculateRecommendationScore(FYPProject project, User user, UserPreference? userPreferences, List<UserInteraction> userInteractions)
        {
            decimal score = 0;

            // Base score from project performance
            score += project.PerformanceScore * 0.3m;

            // Category preference score
            if (userPreferences != null)
            {
                try
                {
                    var preferredCategories = JsonSerializer.Deserialize<List<string>>(userPreferences.PreferredCategories);
                    if (preferredCategories != null && preferredCategories.Contains(project.Category))
                    {
                        score += 20;
                    }
                }
                catch
                {
                    // Ignore JSON parsing errors
                }
            }

            // Department match score
            if (user.DepartmentId == project.DepartmentId)
            {
                score += 15;
            }

            // Recency score (newer projects get slight bonus)
            var yearsDifference = DateTime.Now.Year - project.Year;
            score += Math.Max(0, 5 - yearsDifference);

            // Penalize if user has already interacted with this project
            if (userInteractions.Any(ui => ui.ProjectId == project.Id))
            {
                score -= 10;
            }

            return Math.Max(0, score);
        }

        private static string GenerateRecommendationReason(FYPProject project, User user, UserPreference? userPreferences, List<UserInteraction> userInteractions)
        {
            var reasons = new List<string>();

            if (project.PerformanceScore >= 80)
            {
                reasons.Add("high performance score");
            }

            if (userPreferences != null)
            {
                try
                {
                    var preferredCategories = JsonSerializer.Deserialize<List<string>>(userPreferences.PreferredCategories);
                    if (preferredCategories != null && preferredCategories.Contains(project.Category))
                    {
                        reasons.Add("matches your preferred category");
                    }
                }
                catch
                {
                    // Ignore JSON parsing errors
                }
            }

            if (user.DepartmentId == project.DepartmentId)
            {
                reasons.Add("from your department");
            }

            if (project.Year >= DateTime.Now.Year - 2)
            {
                reasons.Add("recent project");
            }

            return reasons.Any() ? $"Recommended because it has {string.Join(", ", reasons)}" : "Recommended based on your profile";
        }

        private static decimal CalculateSimilarityScore(FYPProject originalProject, FYPProject compareProject)
        {
            decimal score = 0;

            if (originalProject.Category == compareProject.Category)
                score += 40;

            if (originalProject.DepartmentId == compareProject.DepartmentId)
                score += 30;

            if (originalProject.DifficultyLevel == compareProject.DifficultyLevel)
                score += 20;

            // Performance similarity
            var performanceDiff = Math.Abs(originalProject.PerformanceScore - compareProject.PerformanceScore);
            score += Math.Max(0, 10 - performanceDiff / 10);

            return score;
        }
    }
}