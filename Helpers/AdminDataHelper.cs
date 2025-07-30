using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codeline_HealthCareCenter_OOP.Models;


namespace Codeline_HealthCareCenter_OOP.Helpers
{
    public static class AdminDataHelper
    {
        private static readonly string _filePath = "data/admins.txt";

        public static void Save(List<Admin> admins)
        {
            Directory.CreateDirectory("data");
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                foreach (var a in admins)
                {
                    writer.WriteLine($"{a.FullName},{a.Email},{a.Password},{a.PhoneNumber},{a.AdminID}");
                }
            }
        }

        public static List<Admin> Load()
        {
            List<Admin> admins = new List<Admin>();
            if (!File.Exists(_filePath)) return admins;

            foreach (var line in File.ReadAllLines(_filePath))
            {
                var parts = line.Split(',');
                if (parts.Length == 5)
                {
                    admins.Add(new Admin(
                        parts[0], // FullName
                        parts[1], // Email
                        parts[2], // Password
                        parts[3], // PhoneNumber
                        parts[4]  // AdminID
            
                    ));
                }
            }

            return admins;
        }
    }
}
