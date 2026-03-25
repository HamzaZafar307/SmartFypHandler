using SmartFYPHandler.Models.DTOs.Authentication;

namespace SmartFYPHandler.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginDto loginDto);
        Task<AuthResponse> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponse> GoogleLoginAsync(GoogleLoginDto googleLoginDto);
        Task<UserDto?> GetUserByIdAsync(int userId);
    }
}
