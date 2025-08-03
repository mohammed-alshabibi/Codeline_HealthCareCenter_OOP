using System;
using System.Threading.Tasks;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.Implementations;
using Codeline_HealthCareCenter_OOP.Menus;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IAuthService authService = new AuthService();
            IUserService userService = new UserService();
            IClinicService clinicService = new ClinicService();
            IBookingService bookingService = new BookingService();
            IPatientRecordService patientRecordService = new PatientRecordService();
            IBranchService branchService = new BranchService();
            IBranchDepartmentService branchDepartmentService = new BranchDepartmentService();
            IPatientService patientService = new PatientService(bookingService, patientRecordService, authService);
            IDoctorService doctorService = new DoctorService();


            while (true)
            {
                Console.Clear();

                // Top border
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╔════════════════════════════════════════════════════╗");

                // Header content with different colors per word
                Console.Write("║         ");

                string[] words = { "Welcome", "to", "Codeline", "HealthCare", "System" };
                ConsoleColor[] colors = {
            ConsoleColor.White,
            ConsoleColor.Blue,
            ConsoleColor.Cyan,
            ConsoleColor.Red,
            ConsoleColor.Gray
        };

                for (int i = 0; i < words.Length; i++)
                {
                    Console.ForegroundColor = colors[i % colors.Length];
                    Console.Write(words[i] + " ");
                }

                // Fill remaining space to align border
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("     ║");

                // Bottom border
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine();

                // Login title
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  Secure Login");
                Console.ResetColor();

                Console.WriteLine("----------------------------------------------");

                // Email input
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" Email Address: ");
                Console.ForegroundColor = ConsoleColor.White;
                string email = Console.ReadLine();

                // Password input (masked or plain)
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" Password     : ");
                Console.ForegroundColor = ConsoleColor.White;

                string password = Console.ReadLine();
                // string password = ReadPassword(); // Enable this line to use password masking

                Console.WriteLine("----------------------------------------------");
                Console.ResetColor();
                var user = await authService.Login(email, password); // should return UserInputDTO


                // check if is the user is a super admin

           
                  
                if (user != null)
                {
                    switch (user.Role?.ToLower())
                    {
                        case "admin":
                            var adminMenu = new AdminMenu(
                                bookingService,
                                clinicService,
                                patientRecordService,
                                branchService,
                                branchDepartmentService,
                                userService,
                                patientService,
                                authService
                            );
                            adminMenu.Run();
                            break;

                        case "doctor":
                            Console.Write("Enter your Doctor ID: ");
                            int doctorId = int.Parse(Console.ReadLine());

                            var doctorMenu = new DoctorMenu(
                                doctorId,
                                doctorService,
                                patientRecordService,
                                bookingService,
                                clinicService,
                                authService
                            );
                            doctorMenu.Run();
                            break;

                        case "patient":
                            var patientMenu = new PatientMenu(
                                patientService,
                                bookingService,
                                patientRecordService,
                                authService

                            );
                            patientMenu.Show(null); 
                            break;


                        case "superadmin":
                            Console.WriteLine($" Welcome, Super Admin {user.FullName}!");
                            await authService.SaveTokenToCookie("superadmin_login");
                            SuperAdminMenu.Show();
                            break;

                        default:
                            Console.WriteLine("Unknown role. Contact admin.");
                            break;
                    }
                }
                else
                {
                    Console.Write("User not found. Do you want to sign up as a patient? (y/n): ");
                    var choice = Console.ReadLine();
                    if (choice?.ToLower() == "y")
                    {
                        var patientMenu = new PatientMenu(patientService, bookingService, patientRecordService, authService);
                        patientMenu.Show(null);
                        // triggers signup flow
                    }
                }

                Console.WriteLine("\nPress any key to restart...");
                Console.ReadKey();
            }
        }
    }
}
