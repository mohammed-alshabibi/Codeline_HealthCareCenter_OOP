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
                Console.WriteLine("6. Add Patient");
                Console.WriteLine("7. Logout");

                Console.Write("\nSelect Option: ");
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
        private void ShowClinicManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("🔧 Clinic Management");
                Console.WriteLine("1. Add Clinic");
                Console.WriteLine("2. View All Clinics");
                Console.WriteLine("3. Update Clinic");
                Console.WriteLine("4. Delete Clinic");
                Console.WriteLine("5. Back");
                Console.Write("Select an option: ");
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
                        var clinics = _clinicService.GetAllClinics();
                        foreach (var clinic in clinics)
                        {
                            Console.WriteLine($"ID: {clinic.ClinicId} | Name: {clinic.ClinicName} | Dept: {clinic.Department} | Location: {clinic.Location}");
                        }
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
                        Console.WriteLine(deleted ? "Deleted ✅" : "Clinic not found ❌");
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

                Console.WriteLine(" Booking created! Press any key...");
                Console.ReadKey();
            }
        }

        private void ManagePatientRecords()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Patient Records ===");

                var records = _patientRecordService.GetAllRecords(); // always reload the latest

                Console.WriteLine("\n1. View All Records");
                Console.WriteLine("2. Filter by Patient ID");
                Console.WriteLine("3. Add Record");
                Console.WriteLine("4. Back");
                Console.Write("\n> ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("=== All Patient Records ===\n");

                    foreach (var record in records)
                    {
                        Console.WriteLine($"#{record.RecordId} | {record.PatientName} | {record.Diagnosis} | {record.VisitDate.ToShortDateString()}");
                    }

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
                        Console.WriteLine("=== Patient Info ===");
                        Console.WriteLine($"ID: {selectedPatient.PatientID}");
                        Console.WriteLine($"Name: {selectedPatient.FullName}");
                        Console.WriteLine($"Email: {selectedPatient.Email}");
                        Console.WriteLine($"Phone: {selectedPatient.PhoneNumber}");
                        Console.WriteLine($"Gender: {selectedPatient.Gender}");
                        Console.WriteLine($"Age: {selectedPatient.Age}");
                        Console.WriteLine($"National ID: {selectedPatient.NationalID}");

                        var filtered = records
                            .Where(r => r.PatientId == patientId)
                            .ToList();

                        Console.WriteLine("\n=== Patient Records ===");
                        if (filtered.Any())
                        {
                            foreach (var record in filtered)
                            {
                                Console.WriteLine($"#{record.RecordId} | Diagnosis: {record.Diagnosis} | Treatment: {record.Treatment} | Date: {record.VisitDate.ToShortDateString()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine(" No records found for this patient.");
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

                    Console.WriteLine("\nAvailable Patients:");
                    foreach (var p in patients)
                    {
                        Console.WriteLine($"ID: {p.PatientID} | Name: {p.FullName}");
                    }

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

                    Console.Clear();
                    Console.WriteLine("=== New Record Added ===");
                    Console.WriteLine($"Record ID: {input.RecordId}");
                    Console.WriteLine($"Patient ID: {input.PatientId}");
                    Console.WriteLine($"Patient Name: {input.PatientName}");
                    Console.WriteLine($"Diagnosis: {input.Diagnosis}");
                    Console.WriteLine($"Treatment: {input.Treatment}");
                    Console.WriteLine($"Visit Date: {input.VisitDate.ToShortDateString()}");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n Record added and saved!");
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

        private void ManageBranches()
        {
            Console.Clear();
            Console.WriteLine("=== Manage Branches & Departments ===");

            var branches = BranchFileHelper.LoadBranches(); // Load latest from file

            foreach (var branch in branches)
            {
                Console.WriteLine($" Branch: {branch.BranchName} | Location: {branch.Location}");
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

        private void ManageAdmins()
        {
            Console.Clear();
            Console.WriteLine("=== Admin Users ===");

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
                input.Role = "Admin";

                _userService.AddSuperAdmin(input);

                Console.WriteLine(" Admin added! Press any key...");
                Console.ReadKey();
            }
        }
        private string Ask(string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }
        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }
}
