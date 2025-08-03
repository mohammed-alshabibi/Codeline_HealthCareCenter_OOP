using System;
using System.Linq;
using System.Text.RegularExpressions;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Implementations;
using Codeline_HealthCareCenter_OOP.Services;

namespace Codeline_HealthCareCenter_OOP.Menus
{
    public class PatientMenu
    {
        private readonly IPatientService _patientService;
        private readonly IBookingService _bookingService;
        private readonly IPatientRecordService _recordService;
        private readonly IAuthService _authService;

        public PatientMenu(IPatientService patientService, IBookingService bookingService, IPatientRecordService recordService, IAuthService authService)
        {
            _patientService = patientService;
            _bookingService = bookingService;
            _recordService = recordService;
            _authService = authService;
        }
        // Method to display the main menu for patients
        public void Show(PatienoutputDTO patient)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║           Welcome to Patient Portal          ║");
                Console.WriteLine("╠══════════════════════════════════════════════╣");
                Console.ResetColor();

                Console.WriteLine("║  [1] Sign Up                                 ║");
                Console.WriteLine("║  [2] Login                                   ║");
                Console.WriteLine("║  [3] Exit                                    ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");

                Console.Write("\n Choose an option: ");

                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        this.PatientSelfSignup();
                        this.Pause();
                        break;

                    case "2":
                        var dto = new PatientInputDTO
                        {
                            Email = AskEmail("Email"),
                            Password = ReadMaskedInput("Password")
                        };

                        var loggedIn = _patientService.AuthenticatePatient(dto);
                        if (loggedIn != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n Welcome {loggedIn.FullName}");
                            Console.ResetColor();
                            this.Pause();

                            this.ShowPatientMenu(loggedIn); //  fixed
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Invalid login.");
                            Console.ResetColor();
                            this.Pause();
                        }
                        break;

                    case "3":
                        Console.WriteLine(" Goodbye!");
                        return;

                    default:
                        Console.WriteLine(" Invalid option.");
                        this.Pause();
                        break;
                }
            }
        }
        // Method to display the patient menu after successful login
        private void ShowPatientMenu(PatienoutputDTO patient)
        {

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($" {patient.FullName} - Patient Menu");
                Console.ResetColor();

                Console.WriteLine("1.  View Medical Records");
                Console.WriteLine("2.  Book Appointment");
                Console.WriteLine("3.  View My Appointments");
                Console.WriteLine("4.  Logout");

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        var records = _recordService.GetRecordsByPatientName(patient.FullName);
                        Console.WriteLine(" Medical Records:");
                        foreach (var r in records)
                        {
                            Console.WriteLine($" {r.VisitDate:yyyy-MM-dd} | Diagnosis: {r.Diagnosis} | Treatment: {r.Treatment}");
                        }
                        this.Pause();
                        break;

                    case "2":
                        Console.Clear();
                        this.ViewAvailableBookings();
                        Console.WriteLine(" Book Appointment");

                        Console.Write("Clinic ID: ");
                        int clinicId = int.Parse(Console.ReadLine());

                        Console.Write("Department ID: ");
                        int departmentId = int.Parse(Console.ReadLine());

                        Console.Write("Doctor ID (optional, press Enter to skip): ");
                        string doctorInput = Console.ReadLine();
                        int? doctorId = string.IsNullOrWhiteSpace(doctorInput) ? null : int.Parse(doctorInput);

                        Console.Write("Date (yyyy-MM-dd): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        Console.Write("Time (HH:mm): ");
                        TimeSpan time = TimeSpan.Parse(Console.ReadLine());

                        var booking = new BookingInputDTO
                        {
                            ClinicId = clinicId,
                            DepartmentId = departmentId,
                            DoctorId = doctorId,
                            AppointmentDate = date,
                            AppointmentTime = time
                        };

                        _bookingService.BookAppointment(booking, patient.Id_Patient);
                        Console.WriteLine(" Appointment booked.");
                        this.Pause();
                        break;

                    case "3":
                        Console.Clear();
                        var appts = _bookingService.GetBookedAppointments(patient.Id_Patient);
                        Console.WriteLine(" Your Appointments:");
                        foreach (var a in appts)
                        {
                            Console.WriteLine($" {a.AppointmentDate:yyyy-MM-dd} at {a.AppointmentTime} | {a.ClinicName} with {a.DoctorName}");
                        }
                        this.Pause();
                        break;

                    case "4":
                        _authService.Logout().Wait();
                        Console.WriteLine(" Logged out...");
                        return;

                    default:
                        Console.WriteLine(" Invalid option.");
                        this.Pause();
                        break;
                }
            }
        }
        // Method to handle patient self-signup
        public void PatientSelfSignup()
        {
            Console.Clear();
            Console.WriteLine(" Patient Signup");
            Console.WriteLine(" Please fill in the details below:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Note: All fields are required.");
            Console.ResetColor();
            Console.WriteLine("=====================================");

            var input = new PatientInputDTO
            {
                FullName = AskName("Full Name: "),
                Email = AskEmail("Email"),
                Password = ReadMaskedInput("Password: "),
                PhoneNumber = AskInt("Phone Number: "),
                Gender = Ask("Gender: "),
                Age = AskInt("Age: "),
                NationalID = AskInt("National ID: "),
                Id_Patient = 0 // Assuming ID is auto-generated
            };
            _patientService.AddPatient(input);
            Console.WriteLine(" Signup successful.");
        }
        // Method to view available bookings based on clinic and department
        private void ViewAvailableBookings()
        {
            Console.Clear();
            Console.WriteLine("=== View Available Bookings ===");

            Console.Write("Enter Clinic ID: ");
            int clinicId = int.Parse(Console.ReadLine());

            Console.Write("Enter Department ID: ");
            int departmentId = int.Parse(Console.ReadLine());

            var availableSlots = _bookingService.GetAvailableAppointmentsBy(clinicId, departmentId);

            if (!availableSlots.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" No available appointment slots found.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.WriteLine("║          AVAILABLE APPOINTMENT SLOTS           ║");
                Console.WriteLine("╠════════════════════╦═══════════════════════════╣");
                Console.WriteLine("║     Date           ║        Time               ║");
                Console.WriteLine("╠════════════════════╬═══════════════════════════╣");
                Console.ResetColor();

                foreach (var slot in availableSlots)
                {
                    Console.WriteLine($"║ {slot.AppointmentDate:yyyy-MM-dd,-18} ║ {slot.AppointmentTime,-25} ║");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╚════════════════════╩═══════════════════════════╝");
            }

            Console.ResetColor();
            this.Pause();
        }
        // Method to ask for input with validation
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
        // Method to pause the console and wait for user input
        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


    }
}

