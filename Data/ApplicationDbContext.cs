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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Role).HasConversion<int>();
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

            // ProjectMember configuration
            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(pm => pm.Project)
                    .WithMany(p => p.Members)
                    .HasForeignKey(pm => pm.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pm => pm.User)
                    .WithMany(u => u.ProjectMemberships)
                    .HasForeignKey(pm => pm.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Web Development", Description = "Web-based applications and websites" },
                new Category { Id = 2, Name = "Mobile Development", Description = "Mobile applications for iOS and Android" },
                new Category { Id = 3, Name = "Machine Learning", Description = "AI and ML projects" },
                new Category { Id = 4, Name = "Data Science", Description = "Data analysis and visualization projects" },
                new Category { Id = 5, Name = "Desktop Applications", Description = "Desktop software applications" }
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
                    Department = "Computer Science",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            );
        }
    }
}
