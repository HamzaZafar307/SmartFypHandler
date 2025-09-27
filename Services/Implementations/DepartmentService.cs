using Microsoft.EntityFrameworkCore;
using SmartFYPHandler.Data;
using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;

namespace SmartFYPHandler.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments
                .OrderBy(d => d.Name)
                .ToListAsync();

            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            });
        }

        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null) return null;

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt
            };
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            // Check if department code already exists
            var existingDepartment = await _context.Departments
                .FirstOrDefaultAsync(d => d.Code == createDepartmentDto.Code);

            if (existingDepartment != null)
            {
                throw new InvalidOperationException($"Department with code '{createDepartmentDto.Code}' already exists.");
            }

            var department = new Department
            {
                Name = createDepartmentDto.Name,
                Code = createDepartmentDto.Code,
                Description = createDepartmentDto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt
            };
        }

        public async Task<DepartmentDto?> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return null;

            // Check if new code conflicts with existing department
            if (!string.IsNullOrEmpty(updateDepartmentDto.Code) && updateDepartmentDto.Code != department.Code)
            {
                var existingDepartment = await _context.Departments
                    .FirstOrDefaultAsync(d => d.Code == updateDepartmentDto.Code && d.Id != id);

                if (existingDepartment != null)
                {
                    throw new InvalidOperationException($"Department with code '{updateDepartmentDto.Code}' already exists.");
                }
            }

            // Update fields if provided
            if (!string.IsNullOrEmpty(updateDepartmentDto.Name))
                department.Name = updateDepartmentDto.Name;

            if (!string.IsNullOrEmpty(updateDepartmentDto.Code))
                department.Code = updateDepartmentDto.Code;

            if (updateDepartmentDto.Description != null)
                department.Description = updateDepartmentDto.Description;

            department.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt
            };
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            // Check if department has users or projects
            var hasUsers = await _context.Users.AnyAsync(u => u.DepartmentId == id);
            var hasProjects = await _context.FYPProjects.AnyAsync(p => p.DepartmentId == id);

            if (hasUsers || hasProjects)
            {
                throw new InvalidOperationException("Cannot delete department that has associated users or projects.");
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DepartmentExistsAsync(int id)
        {
            return await _context.Departments.AnyAsync(d => d.Id == id);
        }
    }
}