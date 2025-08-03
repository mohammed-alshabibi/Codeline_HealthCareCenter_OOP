using System.Text;
using System.Text.Json;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;


namespace Codeline_HealthCareCenter_OOP.Services
{
    public class AuthService : IAuthService // Implementation of IAuthService
    {
        private const string TokenFilePath = "auth_token.txt";

        public Task SaveTokenToCookie(string token) // Saves the token to a file
        {
            Directory.CreateDirectory(Path.GetDirectoryName(TokenFilePath)); // Ensure the directory exists
            if (string.IsNullOrEmpty(token))
            {
                if (File.Exists(TokenFilePath))
                    File.Delete(TokenFilePath);
                return Task.CompletedTask;
            }
            File.WriteAllText(TokenFilePath, token); // Write the token to the file
            return Task.CompletedTask;
        }

        // Retrieves the user ID from the token file
        public Task<int> GetUserIdFromToken()
        {
            if (!File.Exists(TokenFilePath)) // Check if the token file exists
                return Task.FromResult(-1);

            string token = File.ReadAllText(TokenFilePath); // Read the token from the file
            if (int.TryParse(token, out int userId))
                return Task.FromResult(userId);

            return Task.FromResult(-1); // Return -1 if parsing fails or token is invalid
        }

        // Logs out the user by deleting the token file
        public Task Logout()
        {
            if (File.Exists(TokenFilePath)) // Check if the token file exists
            {
                File.Delete(TokenFilePath); // Delete the token file
            }
            return Task.CompletedTask;
        }

        // Logs in the user by checking credentials against stored data
        public Task<UserInputDTO> Login(string email, string password)
        {
                //  Default SuperAdmin
                if (email.Equals("SA", StringComparison.OrdinalIgnoreCase) && password == "123") // Check for SuperAdmin credentials
                {
                    return Task.FromResult(new UserInputDTO
                    {
                        Email = email,
                        Password = password,
                        Role = "superadmin",
                        FullName = "Super Admin",
                        PhoneNumber = "00000000" 
                    });
                }

                //  Load from files
                var admins = AdminDataHelper.Load();
                var doctors = DoctorDataHelper.Load();
                var patients = PatientDataHelper.Load();


                //  Try to find a match in admins
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

                //  Try to find a match in doctors
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

                //  Try to find a match in patients
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

                //  No match found
                return Task.FromResult<UserInputDTO>(null);
        }
    }
}
