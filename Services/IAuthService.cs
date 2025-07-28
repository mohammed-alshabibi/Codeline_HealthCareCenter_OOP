using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.Shared.Helper;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IAuthService
    {
        JwtTokenResponse GenerateToken(User user);
        Task SaveTokenToCookie(string token);
        Task<int> GetUserIdFromToken();
    }
}