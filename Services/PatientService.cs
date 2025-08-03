using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;
using Codeline_HealthCareCenter_OOP.Menus;
using Codeline_HealthCareCenter_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class PatientService : IPatientService
    {
        private List<Patient> _patients; // List to hold patient data
        private readonly IBookingService _bookingService; // Service for managing bookings
        private readonly IPatientRecordService _recordService; // Service for managing patient records
        private readonly IAuthService _authService; // Service for authentication

        // Load patients from file and initialize services
        public PatientService(IBookingService bookingService, IPatientRecordService recordService, IAuthService authService)
        {
            _bookingService = bookingService;
            _recordService = recordService;
            _patients = PatientDataHelper.Load();
            _authService = authService;
        }
        // Method to retrieve all patients
        public IEnumerable<Patient> GetAllPatients() => _patients;
        // Method to get a patient by their unique ID
        public Patient GetPatientById(int Pid) =>
            _patients.FirstOrDefault(p => p.UserID == Pid);
        // Method to get a patient by their name
        public Patient GetPatientByName(string name) =>
            _patients.FirstOrDefault(p => p.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
        // Method to update a patient's details
        public void UpdatePatientDetails(int uid, int phone)
        {
            var p = _patients.FirstOrDefault(p => p.UserID == uid);
            if (p != null)
                p.PhoneNumber = phone;
        }
        // Method to authenticate a patient using their email and password
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
        // Method to add a new patient
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
        // Method to get available appointments by clinic and department
        public IEnumerable<BookingInputDTO> GetAvailableAppointments(int clinicId, int departmentId)
        {
            return _bookingService.GetAvailableAppointmentsBy(clinicId, departmentId);
        }
    }
}
