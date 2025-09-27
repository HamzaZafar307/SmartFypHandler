using SmartFYPHandler.Models.DTOs;
using SmartFYPHandler.Models.Entities;

namespace SmartFYPHandler.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto);
        Task<DepartmentDto?> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto);
        Task<bool> DeleteDepartmentAsync(int id);
        Task<bool> DepartmentExistsAsync(int id);
    }
}