using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;
using Codeline_HealthCareCenter_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly List<Doctor> _doctors = new();

        // Load the doctors from the file when the service is initialized
        public DoctorService()
        {
            _doctors = DoctorDataHelper.Load();
        }
        // Method to save the current list of doctors to the file
        public void SaveToFile()
        {
            DoctorDataHelper.Save(_doctors);
        }
        // Method to add a new doctor based on the input DTO
        public void AddDoctor(DoctorInput input)
        {
            var doctor = new Doctor
                (
                input.FullName,
                input.Email,
                input.Password,
                input.Specialization,
                input.PhoneNumber,
                input.Gender,
                input.YearsOfExperience,
                input.Salary,
                input.Availability,
                input.DoctorID
            );

            _doctors.Add(doctor);
            DoctorDataHelper.Save(_doctors);
            Console.WriteLine(" Doctor added Successfully.");
        }
        // Method to get all doctors
        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctors;
        }
        // Method to get a doctor by ID
        public Doctor? GetDoctorById(int id) =>
    _doctors.FirstOrDefault(d => int.TryParse(d.doctorID, out int parsedId) && parsedId == id);

        // Method to get a doctor by email
        public Doctor? GetDoctorByEmail(string email) =>
            _doctors.FirstOrDefault(d => d.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        // Method to get a doctor by name
        public Doctor? GetDoctorByName(string docName) =>
            _doctors.FirstOrDefault(d => d.FullName.Equals(docName, StringComparison.OrdinalIgnoreCase));
        // updates an existing doctor's details
        public void UpdateDoctor(Doctor doctor) 
        {
            var existing = _doctors.FirstOrDefault(d => d.doctorID == doctor.doctorID);
            if (existing != null)
            {
                existing.FullName = doctor.FullName;
                existing.Email = doctor.Email;
                existing.Specialization = doctor.Specialization;
                existing.Salary = doctor.Salary;
                existing.Availability = doctor.Availability;
            }
        }
        // Method to get a doctor by branch and department
        public IEnumerable<Doctor> GetDoctorByBrancDep(int bid, int depid)
        {
            return Enumerable.Empty<Doctor>(); // implement later if needed
        }
        // Method to get a doctor's data by name or ID
        public DoctorOutPutDTO? GetDoctorData(string? docName, int? Did)
        {
            var doctor = Did != null
                ? GetDoctorById(Did.Value)
                : docName != null
                    ? GetDoctorByName(docName)
                    : null;

            if (doctor == null) return null;

            return new DoctorOutPutDTO
            {
                FullName = doctor.FullName,
                Email = doctor.Email,
                Specialization = doctor.Specialization,
                Availability = doctor.Availability,
                Salary = doctor.Salary
            };
        }
        // Method to update a doctor's details based on input DTO
        public void UpdateDoctorDetails(DoctorUpdateDTO input)
        {
            var doc = GetDoctorById(input.DoctorId);
            if (doc != null)
            {
                doc.Availability = input.Availability;
                doc.Salary = input.Salary;
            }
        }
        // Method to get doctor details by ID
        public DoctorOutPutDTO? GetDoctorDetailsById(int uid)
        {
            var doc = GetDoctorById(uid);
            if (doc == null) return null;

            return new DoctorOutPutDTO
            {
                FullName = doc.FullName,
                Email = doc.Email,
                Specialization = doc.Specialization,
                Availability = doc.Availability,
                Salary = doc.Salary
            };
        }
        // Method to get doctors by branch name or department name
        public IEnumerable<DoctorOutPutDTO> GetDoctorsByBranchName(string branchName)
        {
            return Enumerable.Empty<DoctorOutPutDTO>(); 
        }
        // Method to get doctors by department name
        public IEnumerable<DoctorOutPutDTO> GetDoctorsByDepartmentName(string departmentName)
        {
            return Enumerable.Empty<DoctorOutPutDTO>(); 
        }
        // Method to check if an email already exists in the doctor list
        public bool EmailExists(string email)
        {
            return _doctors.Any(d => d.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
        // This field is used to store patient records associated with doctors
        private List<PatientRecord> _patientRecords = new();
        // Method to assign a doctor to a clinic and department
        public void AssignToClinic(int doctorId, int clinicId, int departmentId)
        {
            var doc = GetDoctorById(doctorId);
            if (doc != null)
            {
                doc.ClinicId = clinicId;
                doc.DepartmentId = departmentId;
                SaveToFile(); // Save the updated doctor lisT
            }
        }
        // Method to get all patient records for a specific doctor
        public IEnumerable<PatientRecord> GetDoctorPatientRecords(int doctorId)
        {
            return _patientRecords.Where(p => p.DoctorId == doctorId);
        }
        // Method to add or update a patient record for a doctor
        public void AddOrUpdatePatientRecord(int doctorId, PatientRecord record)
        {
            var existing = _patientRecords.FirstOrDefault(p => p.RecordId == record.RecordId);

            if (existing != null)
            {
                existing.Notes = record.Notes;
                existing.VisitDate = record.VisitDate;
    
            }
            else
            {
                record.DoctorId = doctorId;
                record.RecordId = _patientRecords.Count > 0 ? _patientRecords.Max(p => p.RecordId) + 1 : 1;
                _patientRecords.Add(record);
            }
        }

    }
}
