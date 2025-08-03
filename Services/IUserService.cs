using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;


namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IUserService
    {
        int AddStaff(User InputUser); // Method to add a new staff member
        void AddSuperAdmin(UserInputDTO InputUser); // Method to add a new super admin
        void AddUser(User user); // Method to add a new user
        UserOutputDTO? AuthenticateUser(UserInputDTO dto); // Method to authenticate a user
        void DeactivateUser(int uid); // Method to deactivate a user by their unique ID
        bool EmailExists(string email); // Method to check if an email already exists in the user list
        User? GetUserById(int uid); // Method to get a user by their unique ID
        User? GetUserByName(string userName); // Method to get a user by their name
        string? GetUserName(int userId); // Method to get a user's name by their unique ID
        void UpdatePassword(int uid, string currentPassword, string newPassword); // Method to update a user's password
        void UpdateUser(User user); // Method to update a user's details
        UserOutputDTO? GetUserData(string? userName, int? uid); // Method to get user data by name or unique ID
        IEnumerable<UserOutputDTO> GetUserByRole(string roleName); // Method to get users by their role name
    }
}