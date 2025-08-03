using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.Services;

public class UserService : IUserService
{
      private List<User> users = new();
    // Method to add a new super admin
    public void AddSuperAdmin(UserInputDTO input)
    {
        var superAdmin = new User(input.FullName, input.Email, input.Password, input.Role);
        users.Add(superAdmin);
    }
    // Method to add a new staff member
    public int AddStaff(User user)
    {
        users.Add(user);
        return user.UserID;
    }
    // Method to add a new user
    public void AddUser(User user)
    {
        users.Add(user);
    }
    // Method to deactivate a user by their unique ID
    public void DeactivateUser(int uid)
    {
        var user = users.FirstOrDefault(u => u.UserID == uid);
        if (user != null)
            user.IsActive = false;
    }
    // Method to authenticate a user using email and password
    public UserOutputDTO AuthenticateUser(UserInputDTO dto)
    {
        var user = users.FirstOrDefault(u =>
            u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) &&
            u.Password == dto.Password);

        if (user == null) return null;

        return new UserOutputDTO
        {
            FullName = user.FullName,
            Role = user.Role,
            UserID = user.UserID
        };
    }
    // Method to check if an email already exists in the user list
    public bool EmailExists(string email) =>
        users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    // Method to get all users
    public User GetUserById(int uid) =>
        users.FirstOrDefault(u => u.UserID == uid);
    // Method to get a user by their name
    public User GetUserByName(string userName) =>
        users.FirstOrDefault(u => u.FullName.Equals(userName, StringComparison.OrdinalIgnoreCase));
    // Method to get a user's name by their unique ID
    public string GetUserName(int userId) =>
        users.FirstOrDefault(u => u.UserID == userId)?.FullName;
    // Method to update a user's password
    public void UpdatePassword(int uid, string currentPassword, string newPassword)
    {
        var user = users.FirstOrDefault(u => u.UserID == uid && u.Password == currentPassword);
        if (user != null)
            user.Password = newPassword;
    }
    // Method to update a user's details
    public void UpdateUser(User user)
    {
        var existing = users.FirstOrDefault(u => u.UserID == user.UserID);
        if (existing != null)
        {
            existing.FullName = user.FullName;
            existing.Email = user.Email;
        }
    }
    // Method to get user data by name or unique ID
    public UserOutputDTO GetUserData(string? userName, int? uid)
    {
        var user = uid.HasValue
            ? users.FirstOrDefault(u => u.UserID == uid.Value)
            : users.FirstOrDefault(u => u.FullName == userName);

        return user != null
            ? new UserOutputDTO { FullName = user.FullName, Email = user.Email, Role = user.Role }
            : null;
    }
    // Method to get users by their role name
    public IEnumerable<UserOutputDTO> GetUserByRole(string roleName)
    {
        return users
            .Where(u => u.Role.Equals(roleName, StringComparison.OrdinalIgnoreCase))
            .Select(u => new UserOutputDTO
            {
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role
            });
    }
}
