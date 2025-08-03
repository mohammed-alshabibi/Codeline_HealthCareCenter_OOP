using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;
namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IAuthService
    {
     
        Task SaveTokenToCookie(string token); // Saves the token to a file
        Task<int> GetUserIdFromToken(); // Retrieves the user ID from the token file
        Task<UserInputDTO> Login(string email, string password); // Logs in the user by checking credentials against stored data
        Task Logout(); // Logs out the user by deleting the token file

    }
}