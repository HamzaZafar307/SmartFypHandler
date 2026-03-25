using SmartFYPHandler.Data;
using SmartFYPHandler.Models.DTOs.Authentication;
using SmartFYPHandler.Models.Entities;
using SmartFYPHandler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SmartFYPHandler.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, ITokenService tokenService, IConfiguration configuration)
        {
            _context = context;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<AuthResponse> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower() && u.IsActive);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var token = _tokenService.GenerateJwtToken(user);
            var userDto = MapToUserDto(user);

            return new AuthResponse
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                User = userDto
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterDto registerDto)
        {
            // Check if user already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == registerDto.Email.ToLower());

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            // Check if student ID already exists (for students)
            if (registerDto.Role == UserRole.Student && !string.IsNullOrEmpty(registerDto.StudentId))
            {
                var existingStudent = await _context.Users
                    .FirstOrDefaultAsync(u => u.StudentId == registerDto.StudentId);

                if (existingStudent != null)
                {
                    throw new InvalidOperationException("Student with this ID already exists");
                }
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                StudentId = registerDto.StudentId,
                Role = registerDto.Role,
                Department = registerDto.Department,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.GenerateJwtToken(user);
            var userDto = MapToUserDto(user);

            return new AuthResponse
            {
                Success = true,
                Message = "Registration successful",
                Token = token,
                User = userDto
            };
        }

        public async Task<AuthResponse> GoogleLoginAsync(GoogleLoginDto googleLoginDto)
        {
            var settings = new Google.Apis.Auth.GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["Google:ClientId"] }
            };

            try
            {
                var payload = await Google.Apis.Auth.GoogleJsonWebSignature.ValidateAsync(googleLoginDto.IdToken, settings);
                
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.GoogleId == payload.Subject || u.Email.ToLower() == payload.Email.ToLower());

                if (user == null)
                {
                    // Auto-register new Google user
                    user = new User
                    {
                        FirstName = payload.GivenName ?? "Google",
                        LastName = payload.FamilyName ?? "User",
                        Email = payload.Email.ToLower(),
                        GoogleId = payload.Subject,
                        Role = UserRole.Student, // Default role
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Department = "Unassigned"
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                else if (string.IsNullOrEmpty(user.GoogleId))
                {
                    // Link existing email-based account to Google
                    user.GoogleId = payload.Subject;
                    user.UpdatedAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }

                var token = _tokenService.GenerateJwtToken(user);
                var userDto = MapToUserDto(user);

                return new AuthResponse
                {
                    Success = true,
                    Message = "Google login successful",
                    Token = token,
                    User = userDto
                };
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Google authentication failed: " + ex.Message);
            }
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive);

            return user != null ? MapToUserDto(user) : null;
        }

        private static UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                StudentId = user.StudentId,
                Role = user.Role.ToString(),
                Department = user.Department
            };
        }
    }
}
