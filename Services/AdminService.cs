using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;
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
        private List<Admin> _admins;

        public AdminService()
        {
            _admins = AdminDataHelper.Load();
        }
        public UserOutputDTO? Login(string email, string password)
        {
            var admin = _admins.FirstOrDefault(a =>
                a.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                a.Password == password);

            if (admin == null) return null;

            return new UserOutputDTO
            {
                FullName = admin.FullName,
                Email = admin.Email,
                Role = admin.Role
            };
        }
        public void AddAdmin(UserInputDTO input)
        {
            string phoneNumber = "00000000"; // You can also collect this from UI
            string adminId = "ADM" + (_admins.Count + 1).ToString("D3");

            Admin newAdmin = new Admin(input.FullName, input.Email, input.Password, phoneNumber, adminId);
            _admins.Add(newAdmin);
            AdminDataHelper.Save(_admins);

            Console.WriteLine(" Admin created and saved.");
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
