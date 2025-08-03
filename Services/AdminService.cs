using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;
using Codeline_HealthCareCenter_OOP.Menus;
using Codeline_HealthCareCenter_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class AdminService
    {
        private List<Admin> _admins; // List to store admin data

        public AdminService()
        {
            _admins = AdminDataHelper.Load(); // Load existing admins from file
        }

        // Authenticates an admin user based on email and password.
        public UserOutputDTO? LoginUserOutputDTO(string email, string password)
        {
            
            var admin = _admins.FirstOrDefault(a =>
                a.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                a.Password == password); // Check if admin exists with matching email and password

            if (admin == null) return null;

            return new UserOutputDTO
            {
                FullName = admin.FullName,
                Email = admin.Email,
                Role = admin.Role
            };
        }
        // Method to add a new admin
        public void AddAdmin(UserInputDTO input) 
        {
            string phoneNumber = "00000000"; // You can also collect this from UI
            string adminId = "ADM" + (_admins.Count + 1).ToString("D3");

            Admin newAdmin = new Admin(input.FullName, input.Email, input.Password, phoneNumber, adminId); // Create a new Admin object
            _admins.Add(newAdmin);
            AdminDataHelper.Save(_admins);

            Console.WriteLine(" Admin created and saved.");
        }
        // Method to display all registered admins
        public void ShowAllAdmins() 
        {
            Console.Clear();

            // Header
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║              Registered Admins List                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.ResetColor();

            if (_admins.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  No admins found.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n Total Admins: {_admins.Count}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                Console.WriteLine($"{" Name",-25} | {" Email",-25} |  ID");
                Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                Console.ResetColor();

                foreach (var admin in _admins)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{admin.FullName,-25} | {admin.Email,-25} | {admin.AdminID}");
                }

                Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            }

        }

    }
}
