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
            IPatientService patientService = new PatientService(bookingService, patientRecordService);
            IDoctorService doctorService = new DoctorService();


            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== Welcome to Codeline HealthCare System ===");
                Console.ResetColor();

                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                var user = await authService.Login(email, password); // should return UserInputDTO

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
                                userService
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
                                clinicService
                            );
                            doctorMenu.Run();
                            break;

                        case "patient":
                            var patientMenu = new PatientMenu(
                                patientService,
                                bookingService,
                                patientRecordService
                            );
                            patientMenu.Show(null); // Or pass patient DTO if needed
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
                        var patientMenu = new PatientMenu(patientService, bookingService, patientRecordService);
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
