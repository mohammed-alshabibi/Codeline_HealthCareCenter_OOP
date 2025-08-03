using System;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;
namespace Codeline_HealthCareCenter_OOP.Menus
{
    public class DoctorMenu
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientRecordService _recordService;
        private readonly IBookingService _bookingService;
        private readonly IAuthService _authService;


        private readonly int _doctorId;

        public DoctorMenu(int doctorId,IDoctorService doctorService, IPatientRecordService recordService, IBookingService bookingService, IClinicService clinicService, IAuthService authService)

        {
            _doctorId = doctorId;
            _doctorService = doctorService;
            _recordService = recordService;
            _bookingService = bookingService;
            _authService = authService;
        }

        // Method to run the Doctor Menu
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║               DOCTOR MENU                    ║");
                Console.WriteLine("╠══════════════════════════════════════════════╣");
                Console.ResetColor();

                Console.WriteLine("║  [1] Assign to Clinic                        ║");
                Console.WriteLine("║  [2] View Patient Records                    ║");
                Console.WriteLine("║  [3] Add/Edit Patient Record                 ║");
                Console.WriteLine("║  [4] Create Appointment                      ║");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("║  [5]  Logout                                 ║");
                Console.ResetColor();

                Console.WriteLine("╚══════════════════════════════════════════════╝");

                Console.Write("\n🔸 Choose an option: ");


                switch (Console.ReadLine())
                {
                    case "1": AssignToClinic(); break;
                    case "2": ViewRecords(); break;
                    case "3": AddOrEditRecord(); break;
                    case "4": CreateAppointment(); break;
                    case "5":
                        _authService.Logout();
                        Console.WriteLine(" Logged out successfully! Press any key to return to login...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // Method to assign the doctor to a clinic and department
        private void AssignToClinic()
        {
            Console.Write("Enter Clinic ID: ");
            string? clinicInput = Console.ReadLine();
            if (!int.TryParse(clinicInput, out int clinicId))
            {
                Console.WriteLine("Invalid Clinic ID. Please enter a valid number.");
                return;
            }
            Console.Write("Enter Department ID: ");
            string? deptInput = Console.ReadLine();
            if (!int.TryParse(deptInput, out int deptId))
            {
                Console.WriteLine("Invalid Department ID. Please enter a valid number.");
                return;
            }

            _doctorService.AssignToClinic(_doctorId, clinicId, deptId);
            Console.WriteLine(" Assigned successfully! Press any key...");
            Console.ReadKey();
        }
        // Method to view all patient records for the doctor
        private void ViewRecords()
        {
            Console.Clear();
            Console.WriteLine("=== Patient Records ===");
            var records = _doctorService.GetDoctorPatientRecords(_doctorId);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                   PATIENT RECORDS                                                  ║");
            Console.WriteLine("╠════════════╦════════════════════╦══════════════════════╦══════════════════════╦════════════════════╣");
            Console.WriteLine("║ Record ID  ║ Patient Name       ║ Diagnosis            ║ Treatment            ║ Visit Date         ║");
            Console.WriteLine("╠════════════╬════════════════════╬══════════════════════╬══════════════════════╬════════════════════╣");
            Console.ResetColor();

            foreach (var r in records)
            {
                Console.WriteLine($"║ {r.RecordId,-10} ║ {r.PatientName,-18} ║ {r.Diagnosis,-20} ║ {r.Treatment,-20} ║ {r.VisitDate:yyyy-MM-dd}     ║");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚════════════╩════════════════════╩══════════════════════╩══════════════════════╩════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
        }
        // Method to add or edit a patient record
        private void AddOrEditRecord()
        {
            Console.Clear();
            Console.WriteLine("=== Add/Edit Patient Record ===");

            Console.Write("Enter Record ID (0 to create new): ");
            string? recordIdInput = Console.ReadLine();
            if (!int.TryParse(recordIdInput, out int recordId))
            {
                Console.WriteLine("Invalid Record ID.");
                return;
            }

            var input = new PatientRecordInputDTO();

            Console.Write("Patient ID: ");
            string? patientIdInput = Console.ReadLine();
            if (!int.TryParse(patientIdInput, out int parsedPatientId))
            {
                Console.WriteLine("Invalid Patient ID.");
                return;
            }
            input.PatientId = parsedPatientId;

            Console.Write("Patient Name: ");
            input.PatientName = Console.ReadLine() ?? "";

            Console.Write("Diagnosis: ");
            input.Diagnosis = Console.ReadLine() ?? "";

            Console.Write("Treatment: ");
            input.Treatment = Console.ReadLine() ?? "";

            Console.Write("Visit Date (yyyy-MM-dd): ");
            string? visitDateInput = Console.ReadLine();
            if (!DateTime.TryParse(visitDateInput, out DateTime parsedVisitDate))
            {
                Console.WriteLine("Invalid Visit Date.");
                return;
            }
            input.VisitDate = parsedVisitDate;

            Console.Write("Notes (optional): ");
            input.Notes = Console.ReadLine() ?? "";

            if (recordId == 0)
            {
                _doctorService.AddOrUpdatePatientRecord(_doctorId, new Models.PatientRecord
                {
                    PatientId = input.PatientId,
                    PatientName = input.PatientName,
                    Diagnosis = input.Diagnosis,
                    Treatment = input.Treatment,
                    VisitDate = input.VisitDate,
                    Notes = input.Notes
                });
            }
            else
            {
                _recordService.UpdateRecord(recordId, input);
            }
        }

        // Method to create a new appointment
        private void CreateAppointment()
        {
            Console.Clear();
            Console.WriteLine("=== Create Appointment ===");

            var input = new BookingInputDTO();

            // Patient ID
            Console.Write("Patient ID: ");
            string? patientIdInput = Console.ReadLine();
            if (!int.TryParse(patientIdInput, out int patientId))
            {
                Console.WriteLine("Invalid Patient ID.");
                return;
            }

            // Clinic ID
            Console.Write("Clinic ID: ");
            string? clinicInput = Console.ReadLine();
            if (!int.TryParse(clinicInput, out int clinicId))
            {
                Console.WriteLine("Invalid Clinic ID.");
                return;
            }
            input.ClinicId = clinicId;

            // Department ID
            Console.Write("Department ID: ");
            string? deptInput = Console.ReadLine();
            if (!int.TryParse(deptInput, out int deptId))
            {
                Console.WriteLine("Invalid Department ID.");
                return;
            }
            input.DepartmentId = deptId;

            // Doctor ID is injected
            input.DoctorId = _doctorId;

            // Appointment Date
            Console.Write("Appointment Date (yyyy-MM-dd): ");
            string? dateInput = Console.ReadLine();
            if (!DateTime.TryParse(dateInput, out DateTime appDate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }
            input.AppointmentDate = appDate;

            // Appointment Time
            Console.Write("Appointment Time (hh:mm): ");
            string? timeInput = Console.ReadLine();
            if (!TimeSpan.TryParse(timeInput, out TimeSpan appTime))
            {
                Console.WriteLine("Invalid time format.");
                return;
            }
            input.AppointmentTime = appTime;

            _bookingService.BookAppointment(input, patientId);
            Console.WriteLine("Appointment created! Press any key...");
            Console.ReadKey();
        }

    }
}
