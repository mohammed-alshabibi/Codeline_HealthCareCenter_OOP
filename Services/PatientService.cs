using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;
using Codeline_HealthCareCenter_OOP.Menus;
using Codeline_HealthCareCenter_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class PatientService : IPatientService
    {
        private List<Patient> _patients;
        private readonly IBookingService _bookingService;
        private readonly IPatientRecordService _recordService;
        private readonly IAuthService _authService;

        public PatientService(IBookingService bookingService, IPatientRecordService recordService, IAuthService authService)
        {
            _bookingService = bookingService;
            _recordService = recordService;
            _patients = PatientDataHelper.Load();
            _authService = authService;
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
                NationalID = patient.NationalID,
                Id_Patient = patient.UserID
            };
        }

        public void AddPatient(PatientInputDTO input)
        {
            int nextId = _patients.Any() ? _patients.Max(p => p.UserID) + 1 : 1;

            var newPatient = new Patient(
                input.FullName,
                input.Email,
                input.Password,
                input.PhoneNumber,
                input.Gender,
                input.Age,
                input.NationalID,
                nextId
            );

            _patients.Add(newPatient);
            PatientDataHelper.Save(_patients);
            Console.WriteLine(" Patient added successfully.");
        }

        public IEnumerable<BookingInputDTO> GetAvailableAppointments(int clinicId, int departmentId)
        {
            return _bookingService.GetAvailableAppointmentsBy(clinicId, departmentId);
        }

        //  Moved to public so we can call it from Main
        public async Task PatientLogin(IAuthService authService)
        {
            Console.WriteLine("=== Patient Login ===");

            var input = new PatientInputDTO
            {
                Email = Ask("Email"),
                Password = Ask("Password")
            };

            var patient = AuthenticatePatient(input);

            if (patient != null)
            {
                Console.WriteLine($" Welcome, {patient.FullName}!");
                await authService.SaveTokenToCookie("patient_login");

                //  Use instance-based PatientMenu
                var patientMenu = new PatientMenu(this, _bookingService, _recordService, _authService);
                patientMenu.Show(patient);
            }
            else
            {
                Console.WriteLine(" Invalid Patient credentials.");
                await authService.SaveTokenToCookie("unauthorized");
            }
        }

        //  Helper method for asking input
        private static string Ask(string label)
        {
            Console.Write($"{label}: ");
            return Console.ReadLine();
        }

        // Optional: self signup from patient
        public void PatientSelfSignup()
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

            AddPatient(input);
            Console.WriteLine(" Patient signed up successfully.");
        }

        public void AddPatientFromAdmin()
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

            AddPatient(input);
            Console.WriteLine("Patient added by admin successfully.");
        }
    }
}
