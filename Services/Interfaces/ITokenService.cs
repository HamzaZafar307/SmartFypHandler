using SmartFYPHandler.Models.Entities;

namespace SmartFYPHandler.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
