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
            AuthService _auth = new AuthService();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" SUPER ADMIN MENU");
                Console.ResetColor();

                Console.WriteLine("1.Create Admin");
                Console.WriteLine("2.View All Admins");
                Console.WriteLine("3.Add Doctors ");

                Console.WriteLine("4.Create Branch");
                Console.WriteLine("5.Create Department");
                Console.WriteLine("6.Assign Department to Branch");

                Console.WriteLine("7.View All Branches");
                Console.WriteLine("8.View All Departments");

                Console.WriteLine("9.Logout");

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
                        Console.WriteLine("admin created");
                        adminService.AddAdmin(adminDto); //  Use DTO-based version
                        Pause();
                        break;
   
                    case "2":
                        adminService.ShowAllAdmins();
                        Pause();
                        break;
                    case "3":
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
            
                    case "4":
                        branchService.AddBranchFromInput();
                        Pause();
                        break;
                    case "5":
                        departmentService.AddDepartment();
                        Pause();
                        break;
                    case "6":
                        branchDepartmentService.AssignDepartmentToBranch();
                        Pause();
                        break;
                    case "7":
                        branchService.ShowAllBranches();
                        Pause();
                        break;
                    case "8":
                        departmentService.ShowDepartments();
                        Pause();
                        break;
                    case "9":
                        _auth.Logout();
                        Console.WriteLine(" Logged out...");
                        return;
                    default:
                        Console.WriteLine(" Invalid choice. Try again.");
                        Pause();
                        break;
                }
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
