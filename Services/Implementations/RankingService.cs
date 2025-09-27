using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SmartFYPHandler.Data;
using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations
{
    public class RankingService : IRankingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);

        public RankingService(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<DepartmentRankingDto>> GetDepartmentRankingsAsync(int departmentId, int? year = null, string? semester = null)
        {
            var cacheKey = $"department_rankings_{departmentId}_{year}_{semester}";

            if (_cache.TryGetValue(cacheKey, out List<DepartmentRankingDto>? cachedRankings) && cachedRankings != null)
            {
                return cachedRankings;
            }

            var query = _context.FYPProjects
                .Include(p => p.Department)
                .Where(p => p.DepartmentId == departmentId && p.Status == ProjectStatus.Completed);

            if (year.HasValue)
                query = query.Where(p => p.Year == year.Value);

            if (!string.IsNullOrEmpty(semester))
                query = query.Where(p => p.Semester == semester);

            var projects = await query
                .OrderByDescending(p => p.PerformanceScore)
                .ThenByDescending(p => p.Citations)
                .ToListAsync();

            var rankings = projects
                .Select((project, index) => new DepartmentRankingDto
                {
                    Rank = index + 1,
                    ProjectId = project.Id,
                    ProjectTitle = project.Title,
                    DepartmentName = project.Department?.Name ?? "",
                    PerformanceScore = project.PerformanceScore,
                    FinalGrade = project.FinalGrade,
                    Year = project.Year,
                    Semester = project.Semester
                })
                .ToList();

            _cache.Set(cacheKey, rankings, _cacheExpiration);
            return rankings;
        }

        public async Task<IEnumerable<OverallRankingDto>> GetOverallRankingsAsync(int? year = null, string? semester = null, int? top = null)
        {
            var cacheKey = $"overall_rankings_{year}_{semester}_{top}";

            if (_cache.TryGetValue(cacheKey, out List<OverallRankingDto>? cachedRankings) && cachedRankings != null)
            {
                return cachedRankings;
            }

            var query = _context.FYPProjects
                .Include(p => p.Department)
                .Include(p => p.Supervisor)
                .Where(p => p.Status == ProjectStatus.Completed);

            if (year.HasValue)
                query = query.Where(p => p.Year == year.Value);

            if (!string.IsNullOrEmpty(semester))
                query = query.Where(p => p.Semester == semester);

            var orderedQuery = query
                .OrderByDescending(p => p.PerformanceScore)
                .ThenByDescending(p => p.Citations);

            var projectsQuery = top.HasValue ? orderedQuery.Take(top.Value) : orderedQuery;

            var projects = await projectsQuery.ToListAsync();

            var rankings = projects
                .Select((project, index) => new OverallRankingDto
                {
                    Rank = index + 1,
                    ProjectId = project.Id,
                    ProjectTitle = project.Title,
                    DepartmentName = project.Department?.Name ?? "",
                    SupervisorName = $"{project.Supervisor?.FirstName} {project.Supervisor?.LastName}".Trim(),
                    PerformanceScore = project.PerformanceScore,
                    FinalGrade = project.FinalGrade,
                    Year = project.Year,
                    Semester = project.Semester,
                    Citations = project.Citations
                })
                .ToList();

            _cache.Set(cacheKey, rankings, _cacheExpiration);
            return rankings;
        }

        public async Task<RankingStatsDto> GetRankingStatsAsync()
        {
            const string cacheKey = "ranking_stats";

            if (_cache.TryGetValue(cacheKey, out RankingStatsDto? cachedStats) && cachedStats != null)
            {
                return cachedStats;
            }

            var allProjects = await _context.FYPProjects
                .Include(p => p.Department)
                .ToListAsync();

            var completedProjects = allProjects.Where(p => p.Status == ProjectStatus.Completed).ToList();

            var stats = new RankingStatsDto
            {
                TotalProjects = allProjects.Count,
                CompletedProjects = completedProjects.Count,
                InProgressProjects = allProjects.Count(p => p.Status == ProjectStatus.InProgress),
                AveragePerformanceScore = completedProjects.Any() ? completedProjects.Average(p => p.PerformanceScore) : 0,

                ProjectsByDepartment = allProjects
                    .GroupBy(p => p.Department?.Name ?? "Unknown")
                    .ToDictionary(g => g.Key, g => g.Count()),

                ProjectsByCategory = allProjects
                    .GroupBy(p => p.Category)
                    .ToDictionary(g => g.Key, g => g.Count()),

                ProjectsByGrade = completedProjects
                    .Where(p => !string.IsNullOrEmpty(p.FinalGrade))
                    .GroupBy(p => p.FinalGrade)
                    .ToDictionary(g => g.Key, g => g.Count()),

                TopPerformingProjects = completedProjects
                    .OrderByDescending(p => p.PerformanceScore)
                    .Take(5)
                    .Select(p => new TopPerformerDto
                    {
                        ProjectId = p.Id,
                        ProjectTitle = p.Title,
                        DepartmentName = p.Department?.Name ?? "",
                        PerformanceScore = p.PerformanceScore,
                        FinalGrade = p.FinalGrade
                    })
                    .ToList(),

                DepartmentStats = allProjects
                    .GroupBy(p => p.Department?.Name ?? "Unknown")
                    .Select(g => new DepartmentStatsDto
                    {
                        DepartmentName = g.Key,
                        TotalProjects = g.Count(),
                        CompletedProjects = g.Count(p => p.Status == ProjectStatus.Completed),
                        AverageScore = g.Where(p => p.Status == ProjectStatus.Completed).Any()
                            ? g.Where(p => p.Status == ProjectStatus.Completed).Average(p => p.PerformanceScore)
                            : 0,
                        TopGrade = g.Where(p => p.Status == ProjectStatus.Completed && !string.IsNullOrEmpty(p.FinalGrade))
                            .OrderByDescending(p => p.PerformanceScore)
                            .FirstOrDefault()?.FinalGrade ?? "N/A"
                    })
                    .OrderByDescending(d => d.AverageScore)
                    .ToList()
            };

            _cache.Set(cacheKey, stats, _cacheExpiration);
            return stats;
        }

        public async Task UpdateRankingsAsync()
        {
            // Update department rankings
            var departments = await _context.Departments.ToListAsync();

            foreach (var department in departments)
            {
                var projects = await _context.FYPProjects
                    .Where(p => p.DepartmentId == department.Id && p.Status == ProjectStatus.Completed)
                    .OrderByDescending(p => p.PerformanceScore)
                    .ThenByDescending(p => p.Citations)
                    .ToListAsync();

                // Update department rank for each project
                for (int i = 0; i < projects.Count; i++)
                {
                    projects[i].DepartmentRank = i + 1;
                }
            }

            // Update overall rankings
            var allCompletedProjects = await _context.FYPProjects
                .Where(p => p.Status == ProjectStatus.Completed)
                .OrderByDescending(p => p.PerformanceScore)
                .ThenByDescending(p => p.Citations)
                .ToListAsync();

            for (int i = 0; i < allCompletedProjects.Count; i++)
            {
                allCompletedProjects[i].OverallRank = i + 1;
                allCompletedProjects[i].UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            // Clear cache
            _cache.Remove("ranking_stats");

            // Clear department ranking caches
            foreach (var department in departments)
            {
                var departmentCacheKeys = new[]
                {
                    $"department_rankings_{department.Id}__",
                    $"top_department_projects_{department.Id}"
                };

                foreach (var key in departmentCacheKeys)
                {
                    _cache.Remove(key);
                }
            }

            // Clear overall ranking caches
            _cache.Remove("overall_rankings___");
            _cache.Remove("top_overall_projects");
        }

        public async Task<IEnumerable<DepartmentRankingDto>> GetTopProjectsByDepartmentAsync(int departmentId, int top = 10)
        {
            var cacheKey = $"top_department_projects_{departmentId}";

            if (_cache.TryGetValue(cacheKey, out List<DepartmentRankingDto>? cachedTop) && cachedTop != null)
            {
                return cachedTop.Take(top);
            }

            var rankings = await GetDepartmentRankingsAsync(departmentId);
            var topRankings = rankings.Take(top).ToList();

            _cache.Set(cacheKey, rankings.ToList(), _cacheExpiration);
            return topRankings;
        }

        public async Task<IEnumerable<OverallRankingDto>> GetTopProjectsOverallAsync(int top = 10)
        {
            const string cacheKey = "top_overall_projects";

            if (_cache.TryGetValue(cacheKey, out List<OverallRankingDto>? cachedTop) && cachedTop != null)
            {
                return cachedTop.Take(top);
            }

            var rankings = await GetOverallRankingsAsync();
            var topRankings = rankings.Take(top).ToList();

            _cache.Set(cacheKey, rankings.ToList(), _cacheExpiration);
            return topRankings;
        }
    }
}