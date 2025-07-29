using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;
using Codeline_HealthCareCenter_OOP.Menus;
using Codeline_HealthCareCenter_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class PatientService : IPatientService
    {
        private List<Patient> _patients;
        private readonly string _filePath = "data/patients.txt"; // You can delete this if you move to helper
        private readonly IBookingService _bookingService;


        public PatientService()
        {
            _patients = PatientDataHelper.Load();
        }

        public void SaveToFile()
        {
            PatientDataHelper.Save(_patients);
        }
     
        public IEnumerable<Patient> GetAllPatients() => _patients;

        public Patient GetPatientById(int Pid) =>
            _patients.FirstOrDefault(p => p.UserID == Pid);

        public Patient GetPatientByName(string name) =>
            _patients.FirstOrDefault(p => p.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));

        public void UpdatePatientDetails(int uid, string phone)
        {
            var p = _patients.FirstOrDefault(p => p.UserID == uid);
            if (p != null)
                p.PhoneNumber = phone;
        }

        static async Task PatientLogin(IPatientService patientService, IAuthService authService)
        {
            Console.WriteLine("=== Patient Login ===");

            var input = new PatientInputDTO
            {
                Email = Ask("Email:"),
                Password = Ask("Password:")
            };

            var patient = patientService.AuthenticatePatient(input); //  use object, not class

            if (patient != null)
            {
                Console.WriteLine($" Welcome, {patient.FullName}!");
                await authService.SaveTokenToCookie("patient_login"); // use a static label or generate token
                PatientMenu.Show(patient); //  show only on success
            }
            else
            {
                Console.WriteLine(" Invalid Patient credentials.");
                await authService.SaveTokenToCookie("unauthorized");
            }
        }
        public void AddPatient(PatientInputDTO input)
        {
            var newPatient = new Patient
                (
                input.FullName,
                input.Email,
                input.Password,
                input.PhoneNumber,
                input.Gender,
                input.Age,
                input.NationalID
                );

            _patients.Add(newPatient);
            SaveToFile(); // existing method to persist patients

            Console.WriteLine(" Patient added successfully.");
        }
        static void PatientSelfSignup(IPatientService patientService)
        {
            Console.WriteLine("=== Patient Self Signup ===");

            PatientInputDTO input = new()
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
            Console.WriteLine("Patient signed up successfully.");
        }

        static void AddPatientFromAdmin(IPatientService patientService)
        {
            Console.WriteLine("=== Add Patient (By Admin) ===");

            PatientInputDTO input = new()
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
            Console.WriteLine(" Patient added by admin successfully.");
        }


        public PatienoutputDTO AuthenticatePatient(PatientInputDTO dto)
        {
            var patient = _patients.FirstOrDefault(p =>
                p.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) &&
                p.Password == dto.Password);

            if (patient == null)
                return null;

            return new PatienoutputDTO
            {
                FullName = patient.FullName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                Gender = patient.Gender,
                Age = patient.Age,
                NationalID = patient.NationalID
            };
        }
        static string Ask(string label)
        {
            Console.Write($"{label}: ");
            return Console.ReadLine();
        }

        public IEnumerable<BookingInputDTO> GetAvailableAppointments(int clinicId, int departmentId)
        {
            return _bookingService.GetAvailableAppointmentsBy(clinicId, departmentId);
        }

    }
}

