using System;
using Codeline_HealthCareCenter_OOP.Helpers;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.DTO_s;
using HospitalSystem.Services;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class AdminMenu
    {
        private readonly IBookingService _bookingService;
        private readonly IClinicService _clinicService;
        private readonly IPatientRecordService _patientRecordService;
        private readonly IBranchService _branchService;
        private readonly IBranchDepartmentService _branchDepartmentService;
        private readonly IUserService _userService;

        public AdminMenu(
            IBookingService bookingService,
            IClinicService clinicService,
            IPatientRecordService patientRecordService,
            IBranchService branchService,
            IBranchDepartmentService branchDepartmentService,
            IUserService userService)
        {
            _bookingService = bookingService;
            _clinicService = clinicService;
            _patientRecordService = patientRecordService;
            _branchService = branchService;
            _branchDepartmentService = branchDepartmentService;
            _userService = userService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
               
                Console.WriteLine("\n=== ADMIN MENU ===\n");
                Console.WriteLine("1. Manage Clinics");
                Console.WriteLine("2. Manage Bookings");
                Console.WriteLine("3. Manage Patient Records");
                Console.WriteLine("4. Manage Branches & Departments");
                Console.WriteLine("5. Manage Admin Users");
                Console.WriteLine("0. Exit");

                Console.Write("\nSelect Option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ManageClinics();
                        break;
                    case "2":
                        ManageBookings();
                        break;
                    case "3":
                        ManagePatientRecords();
                        break;
                    case "4":
                        ManageBranches();
                        break;
                    case "5":
                        ManageAdmins();
                        break;
                    case "0":
                        Console.WriteLine("Exiting system...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ManageClinics()
        {
            Console.Clear();
            Console.WriteLine("=== Manage Clinics ===");

            _clinicService.GetAllClinics();

            Console.WriteLine("\n1. Add Clinic");
            Console.WriteLine("2. Back");
            Console.Write("\n> ");
            if (Console.ReadLine() == "1")
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Department: ");
                string dept = Console.ReadLine();
                Console.Write("Location: ");
                string loc = Console.ReadLine();

                _clinicService.AddClinic(new ClinicInputDTO
                {
                    ClinicName = name,
                    Department = dept,
                    Location = loc
                });

                Console.WriteLine("✅ Clinic added! Press any key...");
                Console.ReadKey();
            }
        }

        private void ManageBookings()
        {
            Console.Clear();
            Console.WriteLine("=== Manage Bookings ===");

            _bookingService.GetAllBooking();

            Console.WriteLine("\n1. Add Booking");
            Console.WriteLine("2. Back");
            Console.Write("\n> ");
            if (Console.ReadLine() == "1")
            {
                Console.Write("Patient ID: ");
                int patientId = int.Parse(Console.ReadLine());

                var input = new BookingInputDTO();
                Console.Write("Clinic ID: ");
                input.ClinicId = int.Parse(Console.ReadLine());
                Console.Write("Dept ID: ");
                input.DepartmentId = int.Parse(Console.ReadLine());
                Console.Write("Doctor ID: ");
                input.DoctorId = int.Parse(Console.ReadLine());
                Console.Write("Date (yyyy-mm-dd): ");
                input.AppointmentDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Time (hh:mm): ");
                input.AppointmentTime = TimeSpan.Parse(Console.ReadLine());

                _bookingService.BookAppointment(input, patientId);

                Console.WriteLine("✅ Booking created! Press any key...");
                Console.ReadKey();
            }
        }

        private void ManagePatientRecords()
        {
            Console.Clear();
            Console.WriteLine("=== Manage Patient Records ===");

            _patientRecordService.GetAllRecords();

            Console.WriteLine("\n1. Add Record");
            Console.WriteLine("2. Back");
            Console.Write("\n> ");
            if (Console.ReadLine() == "1")
            {
                var input = new PatientRecordInputDTO();
                Console.Write("Patient ID: ");
                input.PatientId = int.Parse(Console.ReadLine());
                Console.Write("Patient Name: ");
                input.PatientName = Console.ReadLine();
                Console.Write("Diagnosis: ");
                input.Diagnosis = Console.ReadLine();
                Console.Write("Treatment: ");
                input.Treatment = Console.ReadLine();
                Console.Write("Visit Date (yyyy-mm-dd): ");
                input.VisitDate = DateTime.Parse(Console.ReadLine());

                _patientRecordService.AddRecord(input);

                Console.WriteLine("✅ Record added! Press any key...");
                Console.ReadKey();
            }
        }

        private void ManageBranches()
        {
            Console.Clear();
            Console.WriteLine("=== Manage Branches & Departments ===");

            var branches = _branchService.GetAllBranches();
            foreach (var branch in branches)
            {
                Console.WriteLine($"🏢 Branch: {branch.BranchName} | Location: {branch.Location}");
            }

            Console.WriteLine("\n1. Add Branch");
            Console.WriteLine("2. Back");
            Console.Write("\n> ");
            if (Console.ReadLine() == "1")
            {
                Console.Write("Branch Name: ");
                string name = Console.ReadLine();
                Console.Write("Location: ");
                string loc = Console.ReadLine();

                _branchService.AddBranch(new BranchDTO
                {
                    BranchName = name,
                    Location = loc
                });

                Console.WriteLine("✅ Branch created! Press any key...");
                Console.ReadKey();
            }
        }

        private void ManageAdmins()
        {
            Console.Clear();
            Console.WriteLine("=== Admin Users ===");

            //  Show all admins using the role filter
            var admins = _userService.GetUserByRole("Admin");
            foreach (var admin in admins)
            {
                Console.WriteLine($"Name: {admin.FullName} | Email: {admin.Email} | Role: {admin.Role}");
            }

            Console.WriteLine("\n1. Add Admin");
            Console.WriteLine("2. Back");
            Console.Write("\n> ");
            if (Console.ReadLine() == "1")
            {
                var input = new UserInputDTO();
                Console.Write("Full Name: ");
                input.FullName = Console.ReadLine();
                Console.Write("Email: ");
                input.Email = Console.ReadLine();
                Console.Write("Password: ");
                input.Password = Console.ReadLine();
                input.Role = "Admin"; // ✅ Force admin role

                //  Add super admin
                _userService.AddSuperAdmin(input);

                Console.WriteLine("✔ Admin added! Press any key...");
                Console.ReadKey();
            }
        }

    }
}

