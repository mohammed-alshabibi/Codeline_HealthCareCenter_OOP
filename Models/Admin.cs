using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Models
{
        public class Admin : User
        {
            public string PhoneNumber { get; set; }
            public string AdminID { get; set; }

            public Admin(string fullName, string email, string password, string phoneNumber, string adminId)
                : base(fullName, email, password, "Admin")
            {
                PhoneNumber = phoneNumber;
                AdminID = adminId;
            }

            public override void DisplayInfo()
            {
                Console.WriteLine($"Admin: {FullName} | Email: {Email} | Phone: {PhoneNumber} | ID: {AdminID}");
            }
        }

}
