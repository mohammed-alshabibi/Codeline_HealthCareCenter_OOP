using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IAuthService
    {
     
        Task SaveTokenToCookie(string token);
        Task<int> GetUserIdFromToken();
    }
}