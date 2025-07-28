using System.Text;
using System.Text.Json;
using HospitalSystem.Services;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class AuthService : IAuthService
    {
        private const string TokenFilePath = "auth_token.txt";

        public Task SaveTokenToCookie(string token)
        {
            File.WriteAllText(TokenFilePath, token);
            return Task.CompletedTask;
        }

        public Task<int> GetUserIdFromToken()
        {
            if (!File.Exists(TokenFilePath))
                return Task.FromResult(-1);

            string token = File.ReadAllText(TokenFilePath);
            if (int.TryParse(token, out int userId))
                return Task.FromResult(userId);

            return Task.FromResult(-1);
        }

        public Task Logout()
        {
            if (File.Exists(TokenFilePath))
                File.Delete(TokenFilePath);

            return Task.CompletedTask;
        }
    }
}
