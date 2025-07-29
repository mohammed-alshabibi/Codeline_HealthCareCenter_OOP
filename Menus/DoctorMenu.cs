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

        private readonly int _doctorId;

        public DoctorMenu(int doctorId, IDoctorService doctorService, IPatientRecordService recordService, IBookingService bookingService)
        {
            _doctorId = doctorId;
            _doctorService = doctorService;
            _recordService = recordService;
            _bookingService = bookingService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Doctor Menu ===");
                Console.WriteLine("1. Assign to Clinic");
                Console.WriteLine("2. View Patient Records");
                Console.WriteLine("3. Add/Edit Patient Record");
                Console.WriteLine("4. Create Appointment");
                Console.WriteLine("5. Back");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1": AssignToClinic(); break;
                    case "2": ViewRecords(); break;
                    case "3": AddOrEditRecord(); break;
                    case "4": CreateAppointment(); break;
                    case "5": return;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AssignToClinic()
        {
            Console.Write("Enter Clinic ID: ");
            int clinicId = int.Parse(Console.ReadLine());
            Console.Write("Enter Department ID: ");
            int deptId = int.Parse(Console.ReadLine());

            _doctorService.AssignToClinic(_doctorId, clinicId, deptId);
            Console.WriteLine("✅ Assigned successfully! Press any key...");
            Console.ReadKey();
        }

        private void ViewRecords()
        {
            Console.Clear();
            Console.WriteLine("=== Patient Records ===");
            var records = _doctorService.GetDoctorPatientRecords(_doctorId);
            foreach (var r in records)
            {
                Console.WriteLine($"Record ID: {r.RecordId} | Name: {r.PatientName} | Diagnosis: {r.Diagnosis} | Treatment: {r.Treatment} | Date: {r.VisitDate:d}");
            }
            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
        }

        private void AddOrEditRecord()
        {
            Console.Clear();
            Console.WriteLine("=== Add/Edit Patient Record ===");
            Console.Write("Enter Record ID (0 to create new): ");
            int recordId = int.Parse(Console.ReadLine());

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

            if (recordId == 0)
                _doctorService.AddOrUpdatePatientRecord(_doctorId, new Models.PatientRecord
                {
                    PatientId = input.PatientId,
                    PatientName = input.PatientName,
                    Diagnosis = input.Diagnosis,
                    Treatment = input.Treatment,
                    VisitDate = input.VisitDate
                });
            else
                _recordService.UpdateRecord(recordId, input);

            Console.WriteLine("✅ Record saved! Press any key...");
            Console.ReadKey();
        }

        private void CreateAppointment()
        {
            Console.Clear();
            Console.WriteLine("=== Create Appointment ===");

            var input = new BookingInputDTO();
            Console.Write("Patient ID: ");
            int patientId = int.Parse(Console.ReadLine());
            Console.Write("Clinic ID: ");
            input.ClinicId = int.Parse(Console.ReadLine());
            Console.Write("Department ID: ");
            input.DepartmentId = int.Parse(Console.ReadLine());
            input.DoctorId = _doctorId;
            Console.Write("Appointment Date (yyyy-mm-dd): ");
            input.AppointmentDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Appointment Time (hh:mm): ");
            input.AppointmentTime = TimeSpan.Parse(Console.ReadLine());

            _bookingService.BookAppointment(input, patientId);

            Console.WriteLine("✅ Appointment created! Press any key...");
            Console.ReadKey();
        }
    }
}
