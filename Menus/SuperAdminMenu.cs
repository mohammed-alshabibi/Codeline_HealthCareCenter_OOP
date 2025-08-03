using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Implementations;
using Codeline_HealthCareCenter_OOP.Services;

namespace Codeline_HealthCareCenter_OOP.Menus
{
    public static class SuperAdminMenu
    {
        // Method to display the Super Admin menu and handle user input
        public static void Show()
        {
            AdminService adminService = new AdminService();
            BranchService branchService = new BranchService();
            DepartmentService departmentService = new DepartmentService();
            DoctorService doctorService = new DoctorService();
            BranchDepartmentService branchDepartmentService = new BranchDepartmentService();
            ClinicService clinicService = new ClinicService();
            AuthService _auth = new AuthService();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║                 SUPER ADMIN MENU                   ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║  Admin Management                                  ║");
                Console.WriteLine("║    [1] Create Admin                                ║");
                Console.WriteLine("║    [2] View All Admins                             ║");
                Console.WriteLine("║                                                    ║");
                Console.WriteLine("║  Doctor Management                                 ║");
                Console.WriteLine("║    [3] Add Doctor                                  ║");
                Console.WriteLine("║                                                    ║");
                Console.WriteLine("║  Branch & Department                               ║");
                Console.WriteLine("║    [4] Create Branch                               ║");
                Console.WriteLine("║    [5] Create Department                           ║");
                Console.WriteLine("║    [6] Assign Department to Branch                 ║");
                Console.WriteLine("║                                                    ║");
                Console.WriteLine("║  View Records                                      ║");
                Console.WriteLine("║    [7] View All Branches                           ║");
                Console.WriteLine("║    [8] View All Departments                        ║");
                Console.WriteLine("║                                                    ║");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("║ [9]  Logout                                        ║");
                Console.ResetColor();
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                Console.Write("\n Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var adminDto = new UserInputDTO
                        {
                            FullName = AskName("Full Name:"),
                            Email = AskEmail("Email:"),
                            Password = ReadMaskedInput("Password:"),
                            PhoneNumber = AskPhone("Phone Number:"),
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
                            FullName = AskName("Full Name:"),
                            Email = AskEmail("Email:"),
                            Password = ReadMaskedInput("Password:"),
                            Specialization = Ask("Specialization:"),
                            PhoneNumber = AskPhone("Phone Number:"),
                            Gender = Ask("Gender:"),
                            YearsOfExperience = AskInt("Years of Experience:"),
                            Salary = AskDouble("Salary:"),
                            Availability = Ask("Availability (e.g., Available/Busy):"),
                            DoctorID = Ask("Doctor ID:")
                        };

                        doctorService.AddDoctor(doctorInput);
                        Pause();
                        break;


                    case "4":
                        BranchDTO branchDto = new BranchDTO
                        {
                            BranchId = AskInt("Enter Branch ID"),
                            BranchName = Ask("Enter Branch Name"),
                            Location = Ask("Enter Branch Location")
                        };


                        branchService.AddBranch(branchDto); 
                        Pause();
                        break;
                    case "5":
                        DepartmentDTO departmentDto = new DepartmentDTO
                        {
                            DepartmentId = AskInt("Enter Department ID"),
                            DepartmentName = Ask("Enter Department Name")
                        };

                        departmentService.CreateDepartment(departmentDto);
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
        // Method to pause the console and wait for user input
        static void Pause()
        {
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
        // Methods to ask for user input with validation
        private static string AskPhone(string label)
        {
            string input;
            do
            {
                Console.Write(label);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, @"^\+?\d{7,15}$"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid phone number. Enter digits only (min 7 digits, optional +).");
                    Console.ResetColor();
                    input = null;
                }
            } while (input == null);

            return input;
        }
        // Method to ask for user input with validation
        private static string Ask(string label, bool required = true)
        {
            string input;
            do
            {
                Console.Write($"{label} ");
                input = Console.ReadLine();
                if (required && string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This field is required. Please try again.");
                    Console.ResetColor();
                }
            } while (required && string.IsNullOrWhiteSpace(input));

            return input;
        }
        // Method to ask for an integer input with validation
        private static int AskInt(string label)
        {
            int value;
            while (true)
            {
                Console.Write($"{label} ");
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number. Please enter a valid integer.");
                Console.ResetColor();
            }
        }
        // Method to ask for a double input with validation
        private static double AskDouble(string label)
        {
            double value;
            while (true)
            {
                Console.Write($"{label} ");
                if (double.TryParse(Console.ReadLine(), out value))
                    return value;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number. Please enter a valid decimal.");
                Console.ResetColor();
            }
        }
        // Method to ask for a name input with validation
        private static string AskName(string label)
        {
            string input;
            do
            {
                Console.Write(label);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || !input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid name (letters and spaces only).");
                    Console.ResetColor();
                    input = null;
                }
            } while (input == null);

            return input;
        }
        // Method to ask for an email input with validation
        private static string AskEmail(string label)
        {
            string input;
            do
            {
                Console.Write(label);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid email format. Try again (e.g., name@example.com).");
                    Console.ResetColor();
                    input = null;
                }
            } while (input == null);

            return input;
        }
        // Method to read masked input (like passwords) from the console
        private static string ReadMaskedInput(string label)
        {
            Console.Write(label);
            string password = "";
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine(); // New line after Enter
            return password;
        }
    }
}
