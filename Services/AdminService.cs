using Codeline_HealthCareCenter_OOP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class AdminService
    {
        private List<Admin> _admins;

        public AdminService()
        {
            _admins = AdminDataHelper.Load();
        }

        public void AddAdmin()
        {
            Console.WriteLine(" Enter Admin Details");

            Console.Write("Full Name: ");
            string fullName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Admin ID: ");
            string adminId = Console.ReadLine();

            Admin newAdmin = new Admin(fullName, email, password, phoneNumber, adminId);
            _admins.Add(newAdmin);

            AdminDataHelper.Save(_admins);
            Console.WriteLine(" Admin added successfully.");
        }

        public void ShowAllAdmins()
        {
            Console.WriteLine("\n Registered Admins:");
            foreach (var admin in _admins)
            {
                Console.WriteLine($" {admin.FullName} | Email: {admin.Email} | ID: {admin.AdminID}");
            }
        }

        public List<Admin> GetAllAdmins()
        {
            return _admins;
        }

        public Admin? Login(string email, string password)
        {
            return _admins.FirstOrDefault(a =>
                a.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                a.Password == password);
        }
    }
}
