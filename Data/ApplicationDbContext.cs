using SmartFYPHandler.Models.DTOs.Authentication;
using SmartFYPHandler.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SmartFYPHandler.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<FYPProject> FYPProjects { get; set; }
        public DbSet<ProjectEvaluation> ProjectEvaluations { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<DepartmentRanking> DepartmentRankings { get; set; }
        public DbSet<UserInteraction> UserInteractions { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Role).HasConversion<int>();

                entity.HasOne(u => u.DepartmentEntity)
                    .WithMany(d => d.Users)
                    .HasForeignKey(u => u.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Department configuration
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Code).IsUnique();
            });

            // Project configuration
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Projects)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Supervisor)
                    .WithMany(u => u.SupervisedProjects)
                    .HasForeignKey(p => p.SupervisorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Status).HasConversion<int>();
            });

            // FYPProject configuration
            modelBuilder.Entity<FYPProject>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(p => p.Department)
                    .WithMany(d => d.FYPProjects)
                    .HasForeignKey(p => p.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Supervisor)
                    .WithMany(u => u.SupervisedFYPProjects)
                    .HasForeignKey(p => p.SupervisorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Status).HasConversion<int>();
            });

            // ProjectMember configuration
            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(pm => pm.Project)
                    .WithMany(p => p.Members)
                    .HasForeignKey(pm => pm.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pm => pm.FYPProject)
                    .WithMany(p => p.ProjectMembers)
                    .HasForeignKey(pm => pm.FYPProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pm => pm.User)
                    .WithMany(u => u.ProjectMemberships)
                    .HasForeignKey(pm => pm.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Role).HasConversion<int>();
            });

            // ProjectEvaluation configuration
            modelBuilder.Entity<ProjectEvaluation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(pe => pe.Project)
                    .WithMany(p => p.ProjectEvaluations)
                    .HasForeignKey(pe => pe.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pe => pe.Evaluator)
                    .WithMany(u => u.ProjectEvaluations)
                    .HasForeignKey(pe => pe.EvaluatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.EvaluationType).HasConversion<int>();
            });

            // DepartmentRanking configuration
            modelBuilder.Entity<DepartmentRanking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(dr => dr.Department)
                    .WithMany(d => d.DepartmentRankings)
                    .HasForeignKey(dr => dr.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(dr => dr.Project)
                    .WithMany(p => p.DepartmentRankings)
                    .HasForeignKey(dr => dr.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // UserInteraction configuration
            modelBuilder.Entity<UserInteraction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(ui => ui.User)
                    .WithMany(u => u.UserInteractions)
                    .HasForeignKey(ui => ui.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ui => ui.Project)
                    .WithMany(p => p.UserInteractions)
                    .HasForeignKey(ui => ui.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.InteractionType).HasConversion<int>();
            });

            // UserPreference configuration
            modelBuilder.Entity<UserPreference>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(up => up.User)
                    .WithOne(u => u.UserPreference)
                    .HasForeignKey<UserPreference>(up => up.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.EngagementLevel).HasConversion<int>();
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Computer Science", Code = "CS", Description = "Computer Science and Software Engineering" },
                new Department { Id = 2, Name = "Software Engineering", Code = "SE", Description = "Software Engineering and Development" },
                new Department { Id = 3, Name = "Data Science", Code = "DS", Description = "Data Science and Analytics" },
                new Department { Id = 4, Name = "Cybersecurity", Code = "CYB", Description = "Cybersecurity and Information Security" },
                new Department { Id = 5, Name = "Information Technology", Code = "IT", Description = "Information Technology and Systems" }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Web Development", Description = "Web-based applications and websites" },
                new Category { Id = 2, Name = "Mobile Development", Description = "Mobile applications for iOS and Android" },
                new Category { Id = 3, Name = "Machine Learning", Description = "AI and ML projects" },
                new Category { Id = 4, Name = "Data Science", Description = "Data analysis and visualization projects" },
                new Category { Id = 5, Name = "Desktop Applications", Description = "Desktop software applications" }
            );

            // Seed Project Categories for FYP
            modelBuilder.Entity<ProjectCategory>().HasData(
                new ProjectCategory { Id = 1, Name = "Machine Learning", Description = "AI and ML related projects" },
                new ProjectCategory { Id = 2, Name = "Web Development", Description = "Web applications and services" },
                new ProjectCategory { Id = 3, Name = "Mobile Development", Description = "Mobile applications for iOS and Android" },
                new ProjectCategory { Id = 4, Name = "IoT", Description = "Internet of Things projects" },
                new ProjectCategory { Id = 5, Name = "Blockchain", Description = "Blockchain and cryptocurrency projects" },
                new ProjectCategory { Id = 6, Name = "Data Science", Description = "Data analysis and visualization" },
                new ProjectCategory { Id = 7, Name = "Cybersecurity", Description = "Security and privacy related projects" }
            );

            // Seed Admin User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "admin@smartfyp.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = UserRole.Admin,
                    DepartmentId = 1,
                    Department = "Computer Science",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            );
        }
    }
}
