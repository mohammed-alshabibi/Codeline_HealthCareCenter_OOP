using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Implementations;
using Codeline_HealthCareCenter_OOP.Services;

namespace Codeline_HealthCareCenter_OOP.Menus
{
    public static class SuperAdminMenu
    {
        public static void Show()
        {
            AdminService adminService = new AdminService();
            BranchService branchService = new BranchService();
            DepartmentService departmentService = new DepartmentService();
            DoctorService doctorService = new DoctorService();
            BranchDepartmentService branchDepartmentService = new BranchDepartmentService();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("🔐 SUPER ADMIN MENU");
                Console.ResetColor();

                Console.WriteLine("1. ➕ Create Admin");
                Console.WriteLine("2. 🔐 Admin Login");
                Console.WriteLine("3. 📋 View All Admins");
                Console.WriteLine("4. ➕ Add Doctors ");
                Console.WriteLine("5. 🔐 Doctor Login");

                Console.WriteLine("6. 🏨 Create Branch");
                Console.WriteLine("7. 🏥 Create Department");
                Console.WriteLine("8. 🔗 Assign Department to Branch");

                Console.WriteLine("9. 📂 View All Branches");
                Console.WriteLine("10. 📂 View All Departments");

                Console.WriteLine("11. 🔙 Logout");

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var adminDto = new UserInputDTO
                        {
                            FullName = Ask("Full Name:"),
                            Email = Ask("Email:"),
                            Password = Ask("Password:"),
                            PhoneNumber = Ask("Phone Number:"),
                            Role = "Admin"
                        };

                        adminService.AddAdmin(adminDto); //  Use DTO-based version
                        Pause();
                        break;
                    case "2":
                        AdminLogin(adminService);
                        break;
                    case "3":
                        adminService.ShowAllAdmins();
                        Pause();
                        break;
                    case "4":
                        var doctorInput = new DoctorInput
                        {
                            FullName = Ask("Full Name:"),
                            Email = Ask("Email:"),
                            Password = Ask("Password:"),
                            Specialization = Ask("Specialization:"),
                            PhoneNumber = Ask("Phone Number:"),
                            Gender = Ask("Gender:"),
                            YearsOfExperience = int.Parse(Ask("Years of Experience:")),
                            Salary = double.Parse(Ask("Salary:")),
                            Availability = Ask("Availability (e.g., Available/Busy):")
                        };

                        doctorService.AddDoctor(doctorInput);
                        Pause();
                        break;
                    case "5":
                        doctorService.DoctorLogin();
                        Pause();
                        break;
                    case "6":
                        branchService.AddBranchFromInput();
                        Pause();
                        break;
                    case "7":
                        departmentService.AddDepartment();
                        Pause();
                        break;
                    case "8":
                        branchDepartmentService.AssignDepartmentToBranch();
                        Pause();
                        break;
                    case "9":
                        branchService.ShowAllBranches();
                        Pause();
                        break;
                    case "10":
                        departmentService.ShowDepartments();
                        Pause();
                        break;
                    case "11":
                        Console.WriteLine("🔓 Logged out...");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid choice. Try again.");
                        Pause();
                        break;
                }
            }
        }

        static void CreateAdmin(AdminService adminService)
        {
            Console.Clear();
            Console.WriteLine(" Create Admin");

            UserInputDTO input = new UserInputDTO();

            Console.Write("Full Name: ");
            input.FullName = Console.ReadLine();

            Console.Write("Email: ");
            input.Email = Console.ReadLine();

            Console.Write("Password: ");
            input.Password = Console.ReadLine();

            input.Role = "Admin";

            adminService.AddAdmin(input);
            Console.WriteLine(" Admin created.");
        }

        static void AdminLogin(AdminService adminService)
        {
            Console.Clear();
            Console.WriteLine(" Admin Login");

            UserInputDTO input = new UserInputDTO();

            Console.Write("Email: ");
            input.Email = Console.ReadLine();

            Console.Write("Password: ");
            input.Password = Console.ReadLine();

            var result = adminService.Login(input.Email, input.Password);

            if (result != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n Welcome {result.FullName} ({result.Role})");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Invalid login.");
                Console.ResetColor();
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
        private static string Ask(string label)
        {
            Console.Write($"{label} ");
            return Console.ReadLine();
        }

    }
}
