using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;


namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IUserService
    {
        int AddStaff(User InputUser);
        void AddSuperAdmin(UserInputDTO InputUser);
        void AddUser(User user);
        UserOutputDTO AuthenticateUser(UserInputDTO dto);
        void DeactivateUser(int uid);
        bool EmailExists(string email);
        User GetUserById(int uid);
        User GetUserByName(string userName);
        string GetUserName(int userId);
        void UpdatePassword(int uid, string currentPassword, string newPassword);
        void UpdateUser(User user);
        UserOutputDTO GetUserData(string? userName, int? uid);
        IEnumerable<UserOutputDTO> GetUserByRole(string roleName);
    }
}