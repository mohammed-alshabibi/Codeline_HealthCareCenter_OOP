using System;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Implementations;
using Codeline_HealthCareCenter_OOP.Services;

namespace Codeline_HealthCareCenter_OOP.Menus
{
    public static class PatientMenu
    {
        public static void Show()
        {
            PatientService patientService = new PatientService();
            BookingService bookingService = new BookingService();
            PatientRecordService recordService = new PatientRecordService();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🏥 Welcome to Patient Portal");
                Console.ResetColor();

                Console.WriteLine("1. 📝 Sign Up");
                Console.WriteLine("2. 🔐 Login");
                Console.WriteLine("3. ❌ Exit");

                Console.Write("\nChoose an option: ");
                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        PatientSelfSignup(patientService);
                        Pause();
                        break;

                    case "2":
                        var dto = new PatientInputDTO
                        {
                            Email = Ask("Email"),
                            Password = Ask("Password")
                        };

                        var loggedIn = patientService.AuthenticatePatient(dto);
                        if (loggedIn != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n✅ Welcome {loggedIn.FullName}");
                            Console.ResetColor();
                            Pause();
                            ShowPatientMenu(loggedIn, bookingService, recordService);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("❌ Invalid login.");
                            Console.ResetColor();
                            Pause();
                        }
                        break;

                    case "3":
                        Console.WriteLine("👋 Goodbye!");
                        return;

                    default:
                        Console.WriteLine("❌ Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private static void ShowPatientMenu(PatienoutputDTO patient, BookingService bookingService, PatientRecordService recordService)
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
                        var records = recordService.GetRecordsByPatientName(patient.FullName);
                        Console.WriteLine(" Medical Records:");
                        foreach (var r in records)
                        {
                            Console.WriteLine($" {r.VisitDate:yyyy-MM-dd} | Diagnosis: {r.Diagnosis} | Treatment: {r.Treatment}");
                        }
                        Pause();
                        break;

                    case "2":
                        Console.Clear();
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

                        bookingService.BookAppointment(booking, patient.PatientId);
                        Console.WriteLine(" Appointment booked.");
                        Pause();
                        break;

                    case "3":
                        Console.Clear();
                        var appts = bookingService.GetBookedAppointments(patient.PatientId);
                        Console.WriteLine(" Your Appointments:");
                        foreach (var a in appts)
                        {
                            Console.WriteLine($" {a.AppointmentDate:yyyy-MM-dd} at {a.AppointmentTime} | {a.ClinicName} with {a.DoctorName}");
                        }
                        Pause();
                        break;

                    case "4":
                        Console.WriteLine(" Logged out...");
                        return;

                    default:
                        Console.WriteLine(" Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private static void PatientSelfSignup(PatientService patientService)
        {
            Console.Clear();
            Console.WriteLine(" Patient Signup");

            var input = new PatientInputDTO
            {
                FullName = Ask("Full Name"),
                Email = Ask("Email"),
                Password = Ask("Password"),
                PhoneNumber = Ask("Phone Number"),
                Gender = Ask("Gender"),
                Age = int.Parse(Ask("Age")),
                NationalID = Ask("National ID")
            };

            patientService.AddPatient(input);
            Console.WriteLine(" Signup successful.");
        }

        private static string Ask(string label)
        {
            Console.Write($"{label}: ");
            return Console.ReadLine();
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
