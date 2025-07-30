using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;
namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IAuthService
    {
     
        Task SaveTokenToCookie(string token);
        Task<int> GetUserIdFromToken();
        Task<UserInputDTO> Login(string email, string password);

    }
}