
namespace Codeline_HealthCareCenter_OOP.Models
{
    public class User
    {
        private static int _userCounter = 1;

        private int userId;
        private string fullName;
        private string email;
        private string password;
        protected string role;
        internal bool isActive;

        public int UserID => userId;

        public string FullName
        {
            get => fullName;
            set => fullName = string.IsNullOrWhiteSpace(value) ? "Unnamed" : value;
        }

        public string Email
        {
            get => email;
            set => email = value.ToLower();
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string Role
        {
            get => role;
            protected set => role = value;
        }

        public bool IsActive
        {
            get => isActive;
            internal set => isActive = value;
        }

        public User(string fullName, string email, string password, string role)
        {
            userId = _userCounter++;
            FullName = fullName;
            Email = email;
            Password = password;
            Role = role;
            isActive = true;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"[User] {FullName} ({Role}) - {Email} | Active: {IsActive}");
        }
    }

}
