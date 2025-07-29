using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

using Codeline_HealthCareCenter_OOP.Services;

public class UserService : IUserService
{
    private List<User> users = new();

    public void AddSuperAdmin(UserInputDTO input)
    {
        var superAdmin = new User(input.FullName, input.Email, input.Password, input.Role);
        users.Add(superAdmin);
    }

    public int AddStaff(User user)
    {
        users.Add(user);
        return user.UserID;
    }

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public User AuthenticateUser(string email, string password)
    {
        return users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    public void DeactivateUser(int uid)
    {
        var user = users.FirstOrDefault(u => u.UserID == uid);
        if (user != null)
            user.IsActive = false;
    }

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

    public bool EmailExists(string email) =>
        users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    public User GetUserById(int uid) =>
        users.FirstOrDefault(u => u.UserID == uid);

    public User GetUserByName(string userName) =>
        users.FirstOrDefault(u => u.FullName.Equals(userName, StringComparison.OrdinalIgnoreCase));

    public string GetUserName(int userId) =>
        users.FirstOrDefault(u => u.UserID == userId)?.FullName;

    public void UpdatePassword(int uid, string currentPassword, string newPassword)
    {
        var user = users.FirstOrDefault(u => u.UserID == uid && u.Password == currentPassword);
        if (user != null)
            user.Password = newPassword;
    }

    public void UpdateUser(User user)
    {
        var existing = users.FirstOrDefault(u => u.UserID == user.UserID);
        if (existing != null)
        {
            existing.FullName = user.FullName;
            existing.Email = user.Email;
        }
    }

    public UserOutputDTO GetUserData(string? userName, int? uid)
    {
        var user = uid.HasValue
            ? users.FirstOrDefault(u => u.UserID == uid.Value)
            : users.FirstOrDefault(u => u.FullName == userName);

        return user != null
            ? new UserOutputDTO { FullName = user.FullName, Email = user.Email, Role = user.Role }
            : null;
    }

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
    public void AddAdmin(UserInputDTO dto)
    {
        // Convert DTO to Admin object
        Admin newAdmin = new Admin(
            dto.FullName,
            dto.Email,
            dto.Password,
            dto.PhoneNumber,
            GenerateAdminId() // You can write a helper to generate unique IDs
        );

        users.Add(newAdmin); // Add to in-memory list
        Console.WriteLine(" Admin created and saved successfully.");
    }
    private string GenerateAdminId()
    {
        return "ADM" + DateTime.Now.Ticks.ToString().Substring(10);
    }

}
