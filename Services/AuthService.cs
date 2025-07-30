using System.Text;
using System.Text.Json;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.DTO_s;


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

        public Task<UserInputDTO> Login(string email, string password)
        {
            var users = new List<UserInputDTO>
    {
        new UserInputDTO { Email = "admin@example.com", Password = "admin123", Role = "admin" },
        new UserInputDTO { Email = "doctor@example.com", Password = "doc123", Role = "doctor" },
        new UserInputDTO { Email = "patient@example.com", Password = "patient123", Role = "patient" },
        new UserInputDTO { Email = "superadmin@example.com", Password = "superadmin123", Role = "superadmin" }


    };

            var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return Task.FromResult(user);
        }

    }
}
