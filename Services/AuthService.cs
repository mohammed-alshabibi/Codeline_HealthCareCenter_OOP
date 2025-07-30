using System.Text;
using System.Text.Json;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;


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
                // 1. Default SuperAdmin
                if (email.Equals("SA", StringComparison.OrdinalIgnoreCase) && password == "123")
                {
                    return Task.FromResult(new UserInputDTO
                    {
                        Email = email,
                        Password = password,
                        Role = "superadmin"
                    });
                }

                // 2. Load from files
                var admins = AdminDataHelper.Load();
                var doctors = DoctorDataHelper.Load();
                var patients = PatientDataHelper.Load();

                // 3. Try to find a match in admins
                var admin = admins.FirstOrDefault(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && a.Password == password);
                if (admin != null)
                {
                    return Task.FromResult(new UserInputDTO
                    {
                        Email = admin.Email,
                        Password = admin.Password,
                        Role = "admin"
                    });
                }

                // 4. Try to find a match in doctors
                var doctor = doctors.FirstOrDefault(d => d.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && d.Password == password);
                if (doctor != null)
                {
                    return Task.FromResult(new UserInputDTO
                    {
                        Email = doctor.Email,
                        Password = doctor.Password,
                        Role = "doctor"
                    });
                }

                // 5. Try to find a match in patients
                var patient = patients.FirstOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && p.Password == password);
                if (patient != null)
                {
                    return Task.FromResult(new UserInputDTO
                    {
                        Email = patient.Email,
                        Password = patient.Password,
                        Role = "patient"
                    });
                }

                // 6. No match found
                return Task.FromResult<UserInputDTO>(null);
        }
    }
}
