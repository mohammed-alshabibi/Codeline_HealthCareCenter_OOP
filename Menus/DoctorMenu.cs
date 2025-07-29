using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Services;

namespace Codeline_HealthCareCenter_OOP.Menus
{
    public class DoctorMenu
    {
        private readonly IDoctorService _doctorService;

        public DoctorMenu(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public void ShowDoctorMenu(int doctorId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Doctor Menu ===");
                Console.WriteLine("1. Assign to Clinic");
                Console.WriteLine("2. View Patient Records");
                Console.WriteLine("3. Add/Edit Patient Record");
                Console.WriteLine("4. Back");
                Console.Write("Choose an option: ");

                var input = Console.ReadLine();
                if (input == "4") break;

                switch (input)
                {
                    case "1":
                        AssignToClinic(doctorId);
                        break;
                    case "2":
                        ViewPatientRecords(doctorId);
                        break;
                    case "3":
                        AddOrEditPatientRecord(doctorId);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AssignToClinic(int doctorId)
        {
            Console.Write("Enter Clinic ID: ");
            int clinicId = int.Parse(Console.ReadLine());

            Console.Write("Enter Department ID: ");
            int departmentId = int.Parse(Console.ReadLine());

            _doctorService.AssignToClinic(doctorId, clinicId, departmentId);

            Console.WriteLine("Doctor assigned to clinic and department.");
            Console.ReadKey();
        }

        private void ViewPatientRecords(int doctorId)
        {
            Console.Clear();
            var records = _doctorService.GetDoctorPatientRecords(doctorId);
            Console.WriteLine("=== Patient Records ===");

            foreach (var r in records)
            {
                Console.WriteLine($"[ID: {r.RecordId}] {r.PatientName} - Diagnosis: {r.Diagnosis}, Treatment: {r.Treatment}, Date: {r.VisitDate.ToShortDateString()}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void AddOrEditPatientRecord(int doctorId)
        {
            Console.WriteLine("Enter Patient Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Enter Diagnosis: ");
            var diagnosis = Console.ReadLine();

            Console.WriteLine("Enter Treatment: ");
            var treatment = Console.ReadLine();

            Console.WriteLine("Enter Visit Date (yyyy-MM-dd): ");
            var date = DateTime.Parse(Console.ReadLine());

            PatientRecord record = new PatientRecord
            {
                PatientName = name,
                Diagnosis = diagnosis,
                Treatment = treatment,
                VisitDate = date
            };

            _doctorService.AddOrUpdatePatientRecord(doctorId, record);

            Console.WriteLine("Record saved. Press any key...");
            Console.ReadKey();
        }
    }
}
