using System;
using System.Linq;
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

        public PatientMenu(IPatientService patientService, IBookingService bookingService, IPatientRecordService recordService)
        {
            _patientService = patientService;
            _bookingService = bookingService;
            _recordService = recordService;
        }

        public void Show(PatienoutputDTO patient)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" Welcome to Patient Portal");
                Console.ResetColor();

                Console.WriteLine("1.  Sign Up");
                Console.WriteLine("2.  Login");
                Console.WriteLine("3.  Exit");

                Console.Write("\nChoose an option: ");
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
                            Email = this.Ask("Email"),
                            Password = this.Ask("Password")
                        };

                        var loggedIn = _patientService.AuthenticatePatient(dto);
                        if (loggedIn != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n Welcome {loggedIn.FullName}");
                            Console.ResetColor();
                            this.Pause();
                            this.ShowPatientMenu(loggedIn);
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

                        _bookingService.BookAppointment(booking, patient.PatientId);
                        Console.WriteLine(" Appointment booked.");
                        this.Pause();
                        break;

                    case "3":
                        Console.Clear();
                        var appts = _bookingService.GetBookedAppointments(patient.PatientId);
                        Console.WriteLine(" Your Appointments:");
                        foreach (var a in appts)
                        {
                            Console.WriteLine($" {a.AppointmentDate:yyyy-MM-dd} at {a.AppointmentTime} | {a.ClinicName} with {a.DoctorName}");
                        }
                        this.Pause();
                        break;

                    case "4":
                        Console.WriteLine(" Logged out...");
                        return;

                    default:
                        Console.WriteLine(" Invalid option.");
                        this.Pause();
                        break;
                }
            }
        }

        private void PatientSelfSignup()
        {
            Console.Clear();
            Console.WriteLine(" Patient Signup");

            var input = new PatientInputDTO
            {
                FullName = this.Ask("Full Name"),
                Email = this.Ask("Email"),
                Password = this.Ask("Password"),
                PhoneNumber = this.Ask("Phone Number"),
                Gender = this.Ask("Gender"),
                Age = int.Parse(this.Ask("Age")),
                NationalID = this.Ask("National ID")
            };

            _patientService.AddPatient(input);
            Console.WriteLine(" Signup successful.");
        }

        private string Ask(string label)
        {
            Console.Write($"{label}: ");
            return Console.ReadLine();
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

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
                Console.WriteLine("❌ No available appointment slots found.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Available Appointment Slots:");
                foreach (var slot in availableSlots)
                {
                    Console.WriteLine($"- {slot.AppointmentDate:yyyy-MM-dd} at {slot.AppointmentTime}");
                }
            }

            Console.ResetColor();
            this.Pause();
        }
    }
}

