
namespace Codeline_HealthCareCenter_OOP.Models
{
    public class User
    {
        private static int _userCounter = 1;

        private int _userId;
        private string _fullName;
        private string _email;
        private string _password;
        protected string _role;
        internal bool _isActive;

        public int UserID => _userId;

        public string FullName
        {
            get => _fullName;
            set => _fullName = string.IsNullOrWhiteSpace(value) ? "Unnamed" : value;
        }

        public string Email
        {
            get => _email;
            set => _email = value.ToLower();
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string Role
        {
            get => _role;
            protected set => _role = value;
        }

        public bool IsActive
        {
            get => _isActive;
            internal set => _isActive = value;
        }

        public User(string fullName, string email, string password, string role)
        {
            _userId = _userCounter++;
            FullName = fullName;
            Email = email;
            Password = password;
            Role = role;
            _isActive = true;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"[User] {FullName} ({Role}) - {Email} | Active: {IsActive}");
        }
    }

}
