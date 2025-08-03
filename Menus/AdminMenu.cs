using System;
using Codeline_HealthCareCenter_OOP.Helpers;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Menus;
using Codeline_HealthCareCenter_OOP.Models;
using System.Text.RegularExpressions;

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
        private readonly IAuthService _authService;
        private readonly IPatientService _patientService;

        public AdminMenu(
            IBookingService bookingService,
            IClinicService clinicService,
            IPatientRecordService patientRecordService,
            IBranchService branchService,
            IBranchDepartmentService branchDepartmentService,
            IUserService userService,
            IPatientService patientService,
            IAuthService authService)

        {
            _bookingService = bookingService;
            _clinicService = clinicService;
            _patientRecordService = patientRecordService;
            _branchService = branchService;
            _branchDepartmentService = branchDepartmentService;
            _userService = userService;
            _authService = authService;
            _patientService = patientService;
            Console.Title = "Codeline HealthCare Center - Admin Menu";
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        // Method to run the admin menu
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║                   ADMIN MENU                       ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.ResetColor();

                Console.WriteLine("║  [1]  Manage Clinics                               ║");
                Console.WriteLine("║  [2]  Manage Bookings                              ║");
                Console.WriteLine("║  [3]  Manage Patient Records                       ║");
                Console.WriteLine("║  [4]  Manage Branches & Departments                ║");
                Console.WriteLine("║  [5]  Manage Admin Users                           ║");
                Console.WriteLine("║  [6]  Add Patient                                  ║");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("║  [7]  Logout                                       ║");
                Console.ResetColor();

                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                Console.Write("\n Select Option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowClinicManagementMenu();
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
                    case "6":
                        Console.Clear();
                        Console.WriteLine(" Patient Signup");

                        var patient = new PatientInputDTO
                        {
                            FullName = AskName("Full Name"),
                            Email = AskEmail("Email"),
                            Password = ReadMaskedInput("Password"),
                            PhoneNumber = AskInt("Phone Number"),
                            Gender = Ask("Gender"),
                            Age = AskInt("Age"),
                            NationalID = AskInt("National ID")
                        };
                        _patientService.AddPatient(patient);
                        Console.WriteLine(" Patient added! Press any key...");
                        Console.ReadKey();
                        break;
                    case "7":
                        _authService.Logout().Wait();
                        Console.WriteLine("Logout...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // <ethod to show the Clinic management menu
        private void ShowClinicManagementMenu() 
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║            CLINIC MANAGEMENT MENU            ║");
                Console.WriteLine("╠══════════════════════════════════════════════╣");
                Console.ResetColor();

                Console.WriteLine("║  [1]  Add Clinic                             ║");
                Console.WriteLine("║  [2]  View All Clinics                       ║");
                Console.WriteLine("║  [3]  Update Clinic                          ║");
                Console.WriteLine("║  [4]  Delete Clinic                          ║");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("║  [5]  Back                                   ║");
                Console.ResetColor();

                Console.WriteLine("╚══════════════════════════════════════════════╝");

                Console.Write("\n🔸 Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Clinic Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Department: ");
                        string department = Console.ReadLine();
                        Console.Write("Location: ");
                        string location = Console.ReadLine();

                        var newClinic = new ClinicInputDTO
                        {
                            ClinicName = name,
                            Department = department,
                            Location = location
                        };
                        _clinicService.AddClinic(newClinic);
                        break;

                    case "2":
                        Console.Clear();
                        var clinics = _clinicService.GetAllClinics();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine("║                                 CLINIC LIST                                        ║");
                        Console.WriteLine("╠════════════╦════════════════════╦════════════════════╦═════════════════════════════╣");
                        Console.WriteLine("║ Clinic ID  ║ Name               ║ Department         ║ Location                    ║");
                        Console.WriteLine("╠════════════╬════════════════════╬════════════════════╬═════════════════════════════╣");
                        Console.ResetColor();

                        foreach (var clinic in clinics)
                        {
                            Console.WriteLine($"║ {clinic.ClinicId,-10} ║ {clinic.ClinicName,-18} ║ {clinic.Department,-20} ║ {clinic.Location,-26} ║");
                        }

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("╚════════════╩════════════════════╩════════════════════╩═════════════════════════════╝");
                        Console.ResetColor();

                        break;

                    case "3":
                        Console.Write("Clinic ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());
                        Console.Write("New Name: ");
                        string newName = Console.ReadLine();
                        Console.Write("New Department: ");
                        string newDept = Console.ReadLine();
                        Console.Write("New Location: ");
                        string newLoc = Console.ReadLine();

                        var updatedClinic = new ClinicInputDTO
                        {
                            ClinicName = newName,
                            Department = newDept,
                            Location = newLoc
                        };
                        _clinicService.UpdateClinic(updateId, updatedClinic);
                        Console.Write(" updated  ");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Write("Clinic ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        bool deleted = _clinicService.DeleteClinic(deleteId);
                        Console.WriteLine(deleted ? "Deleted " : "Clinic not found ");
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
        // Method to manage Bookings
        private void ManageBookings()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║                MANAGE BOOKINGS MENU                ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.ResetColor();

            var bookings = _bookingService.GetAllBooking(); 

            if (bookings.Any())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════╦════════════════╦════════════════╦════════════════════╗");
                Console.WriteLine("║ ID     ║ Patient Name   ║ Clinic Name    ║ Appointment Date   ║");
                Console.WriteLine("╠════════╬════════════════╬════════════════╬════════════════════╣");
                Console.ResetColor();

                foreach (var b in bookings)
                {
                    Console.WriteLine($"║ {b.BookingId,-6} ║ {b.PatientName,-14} ║ {b.ClinicName,-14} ║ {b.AppointmentDate:yyyy-MM-dd HH:mm} ║");
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╚════════╩════════════════╩════════════════╩════════════════════╝");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nNo bookings found.");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.WriteLine("║ Options:                                           ║");
            Console.WriteLine("║  [1]  Add Booking                                  ║");
            Console.WriteLine("║  [2]  Back                                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\n🔸 Select an option: ");
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

                Console.WriteLine(" Booking created! Press any key...");
                Console.ReadKey();
            }
        }
        // Method to manage patient records
        private void ManagePatientRecords()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║             MANAGE PATIENT RECORDS MENU            ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.ResetColor();

                // Load records
                var records = _patientRecordService.GetAllRecords();

                if (records.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("╔════════════╦════════════════════╦══════════════════════╦══════════════════════╦════════════════════╗");
                    Console.WriteLine("║ Record ID  ║ Patient Name       ║ Diagnosis            ║ Treatment           ║ Visit Date          ║");
                    Console.WriteLine("╠════════════╬════════════════════╬══════════════════════╬══════════════════════╬════════════════════╣");
                    Console.ResetColor();

                    foreach (var r in records)
                    {
                        Console.WriteLine($"║ {r.RecordId,-10} ║ {r.PatientName,-18} ║ {r.Diagnosis,-22} ║ {r.Treatment,-20} ║ {r.VisitDate:yyyy-MM-dd}     ║");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("╚════════════╩════════════════════╩══════════════════════╩══════════════════════╩════════════════════╝");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\nNo patient records found.");
                    Console.ResetColor();
                }

                // Show options
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n╔════════════════════════════════════════════╗");
                Console.WriteLine("║ Options:                                     ║");
                Console.WriteLine("║  [1]  View All Records                       ║");
                Console.WriteLine("║  [2]  Filter by Patient ID                   ║");
                Console.WriteLine("║  [3]  Add Record                             ║");
                Console.WriteLine("║  [4]  Back                                   ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");
                Console.ResetColor();

                Console.Write("\n🔸 Select an option: ");

                Console.Write("\n> ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                     ALL PATIENT RECORDS                                      ║");
                    Console.WriteLine("╠══════╦════════════════════╦══════════════════════╦═══════════════════════════╣");
                    Console.WriteLine("║ ID   ║ Patient Name       ║ Diagnosis            ║ Visit Date                ║");
                    Console.WriteLine("╠══════╬════════════════════╬══════════════════════╬═══════════════════════════╣");
                    Console.ResetColor();

                    foreach (var record in records)
                    {
                        Console.WriteLine($"║ #{record.RecordId,-4} ║ {record.PatientName,-18} ║ {record.Diagnosis,-22} ║ {record.VisitDate:yyyy-MM-dd,-24} ║");
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("╚══════╩════════════════════╩══════════════════════╩══════════════════════════╝");
                    Console.ResetColor();


                    Pause();
                }
                else if (choice == "2")
                {
                    var patients = _patientService.GetAllPatients(); // List<PatientOutputDTO>

                    Console.Write("Enter Patient ID to filter: ");
                    if (int.TryParse(Console.ReadLine(), out int patientId))
                    {
                        var selectedPatient = patients.FirstOrDefault(p => p.PatientID == patientId);
                        if (selectedPatient == null)
                        {
                            Console.WriteLine(" Patient not found.");
                            Pause();
                            continue;
                        }

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("╔════════════════════════════════════════════╗");
                        Console.WriteLine("║              PATIENT INFORMATION           ║");
                        Console.WriteLine("╠════════════════════════════════════════════╣");
                        Console.ResetColor();

                        Console.WriteLine($"║ ID           : {selectedPatient.PatientID,-25}║");
                        Console.WriteLine($"║ Name         : {selectedPatient.FullName,-25}║");
                        Console.WriteLine($"║ Email        : {selectedPatient.Email,-25}║");
                        Console.WriteLine($"║ Phone        : {selectedPatient.PhoneNumber,-25}║");
                        Console.WriteLine($"║ Gender       : {selectedPatient.Gender,-25}║");
                        Console.WriteLine($"║ Age          : {selectedPatient.Age,-25}║");
                        Console.WriteLine($"║ National ID  : {selectedPatient.NationalID,-25}║");

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("╚════════════════════════════════════════════╝");
                        Console.ResetColor();


                        var filtered = records
                            .Where(r => r.PatientId == patientId)
                            .ToList();

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
                        Console.WriteLine("║                         FILTERED PATIENT RECORDS                             ║");
                        Console.WriteLine("╠══════╦════════════════════════╦════════════════════════╦═════════════════════╣");
                        Console.WriteLine("║ ID   ║ Diagnosis              ║ Treatment              ║ Visit Date          ║");
                        Console.WriteLine("╠══════╬════════════════════════╬════════════════════════╬═════════════════════╣");
                        Console.ResetColor();

                        if (filtered.Any())
                        {
                            foreach (var record in filtered)
                            {
                                Console.WriteLine($"║ #{record.RecordId,-4} ║ {record.Diagnosis,-24} ║ {record.Treatment,-24} ║ {record.VisitDate:yyyy-MM-dd}     ║");
                            }

                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("╚══════╩════════════════════════╩════════════════════════╩════════════════════╝");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("║  No matching patient records found.                                        ║");
                            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════╝");
                            Console.ResetColor();
                        }


                        Pause();
                    }
                    else
                    {
                        Console.WriteLine(" Invalid input.");
                        Pause();
                    }
                }
                else if (choice == "3")
                {
                    var input = new PatientRecordInputDTO();
                    Console.Clear();
                    Console.WriteLine("=== Add Patient Record ===");
                    Console.WriteLine("Enter the following details:");

                    input.RecordId = records.Any() ? records.Max(r => r.RecordId) + 1 : 1;

                    var patients = _patientService.GetAllPatients();
                    if (!patients.Any())
                    {
                        Console.WriteLine(" No patients found. Please add a patient first.");
                        Pause();
                        continue;
                    }

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("╔════════════════════════════════════════════╗");
                    Console.WriteLine("║             AVAILABLE PATIENTS             ║");
                    Console.WriteLine("╠════════════╦═══════════════════════════════╣");
                    Console.WriteLine("║ Patient ID ║ Name                          ║");
                    Console.WriteLine("╠════════════╬═══════════════════════════════╣");
                    Console.ResetColor();

                    foreach (var p in patients)
                    {
                        Console.WriteLine($"║ {p.PatientID,-10} ║ {p.FullName,-30} ║");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("╚════════════╩═══════════════════════════════╝");
                    Console.ResetColor();


                    Console.Write("\nEnter Patient ID: ");
                    input.PatientId = int.Parse(Console.ReadLine());

                    var selected = patients.FirstOrDefault(p => p.PatientID == input.PatientId);
                    if (selected == null)
                    {
                        Console.WriteLine(" Patient not found.");
                        Pause();
                        continue;
                    }

                    input.PatientName = selected.FullName;

                    Console.Write("Diagnosis: ");
                    input.Diagnosis = Console.ReadLine();

                    Console.Write("Treatment: ");
                    input.Treatment = Console.ReadLine();

                    Console.Write("Visit Date (yyyy-mm-dd): ");
                    input.VisitDate = DateTime.Parse(Console.ReadLine());

                    // Validations
                    if (input.VisitDate > DateTime.Now)
                    {
                        Console.WriteLine(" Visit date cannot be in the future.");
                        Pause();
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(input.PatientName) || string.IsNullOrWhiteSpace(input.Diagnosis))
                    {
                        Console.WriteLine(" Patient Name and Diagnosis cannot be empty.");
                        Pause();
                        continue;
                    }

                    // Add the record
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Adding record... Please wait...");
                    Console.ResetColor();

                    _patientRecordService.AddRecord(input);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Record added successfully!");
                    Console.ResetColor();
                    Pause();

                    // Reload and show added record
                    records = _patientRecordService.GetAllRecords();

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("╔══════════════════════════════════════════════╗");
                    Console.WriteLine("║              NEW RECORD ADDED                ║");
                    Console.WriteLine("╠══════════════════════════════════════════════╣");
                    Console.ResetColor();

                    Console.WriteLine($"║ Record ID     : {input.RecordId,-25}║");
                    Console.WriteLine($"║ Patient ID    : {input.PatientId,-25}║");
                    Console.WriteLine($"║ Patient Name  : {input.PatientName,-25}║");
                    Console.WriteLine($"║ Diagnosis     : {input.Diagnosis,-25}║");
                    Console.WriteLine($"║ Treatment     : {input.Treatment,-25}║");
                    Console.WriteLine($"║ Visit Date    : {input.VisitDate:yyyy-MM-dd,-25}║");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("╚══════════════════════════════════════════════╝");
                    Console.WriteLine("\n Record added and saved successfully!");
                    Console.ResetColor();

                    Pause();
                }
                else if (choice == "4")
                {
                    break; // Exit to previous menu
                }
                else
                {
                    Console.WriteLine(" Invalid option.");
                    Pause();
                }
            }
        }
        // Method to manage branches and departments
        private void ManageBranches() 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║          MANAGE BRANCHES & DEPARTMENTS             ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.ResetColor();

            // Load and display branches (assuming always has items)
            var branches = BranchFileHelper.LoadBranches();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("╔════════════════════════════════╦════════════════════╗");
            Console.WriteLine("║ Branch Name                    ║ Location           ║");
            Console.WriteLine("╠════════════════════════════════╬════════════════════╣");
            Console.ResetColor();

            foreach (var branch in branches)
            {
                Console.WriteLine($"║ {branch.BranchName,-30} ║ {branch.Location,-18} ║");
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("╚════════════════════════════════╩════════════════════╝");
            Console.ResetColor();

            // Options menu
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n");
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║ Options:                                     ║");
            Console.WriteLine("║  [1]  Add Branch                             ║");
            Console.WriteLine("║  [2]  Back                                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\n Select an option: ");

            Console.Write("\n> ");
            if (Console.ReadLine() == "1")
            {
                Console.Write("Branch Name: ");
                string name = Console.ReadLine();
                Console.Write("Location: ");
                string loc = Console.ReadLine();

                int newId = branches.Count > 0 ? branches.Max(b => b.BranchId) + 1 : 1;

                Branch newBranch = new Branch
                {
                    BranchId = newId,
                    BranchName = name,
                    Location = loc,
                    IsActive = true
                };

                branches.Add(newBranch);

                BranchFileHelper.SaveBranches(branches); // Save updated list to file

                Console.WriteLine(" Branch created and saved successfully. Press any key...");
                Console.ReadKey();
            }
        }
        // Method to manage admin users
        private void ManageAdmins()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                             ADMIN USERS                                    ║");
            Console.WriteLine("╠════════════════════════════╦══════════════════════════════╦════════════════╣");
            Console.WriteLine("║ Name                       ║ Email                        ║ Role           ║");
            Console.WriteLine("╠════════════════════════════╬══════════════════════════════╬════════════════╣");
            Console.ResetColor();

            var admins = _userService.GetUserByRole("Admin");

            foreach (var admin in admins)
            {
                Console.WriteLine($"║ {admin.FullName,-26} ║ {admin.Email,-28} ║ {admin.Role,-12} ║");
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╚════════════════════════════╩══════════════════════════════╩═══════════════╝");
            Console.ResetColor();

            // Options menu
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n");
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║ Options:                                     ║");
            Console.WriteLine("║  [1]  Add Admin                              ║");
            Console.WriteLine("║  [2]  Back                                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\n🔸 Select an option: ");

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
                input.Role = "Admin";

                _userService.AddSuperAdmin(input);

                Console.WriteLine(" Admin added! Press any key...");
                Console.ReadKey();
            }
        }
        // Method to ask a question and return the answer
        private string Ask(string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }
        // Methods to ask for input with validation
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
        // Method to read masked input (like password)
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
        // Method to ask for a valid email input
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
        // Method to pause the console and wait for user input
        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }
}
