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

        public DoctorService()
        {
            _doctors = DoctorDataHelper.Load();
        }

        public void SaveToFile()
        {
            DoctorDataHelper.Save(_doctors);
        }

        public void AddDoctor(DoctorInput input)
        {
            var doctor = new Doctor(
                input.FullName,
                input.Email,
                input.Password,
                input.Specialization,
                input.PhoneNumber,
                input.Gender,
                input.YearsOfExperience,
                input.Salary,
                input.Availability
            );

            _doctors.Add(doctor);
            Console.WriteLine(" Doctor added Successfully.");
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctors;
        }

        // You can implement the rest later as needed
        public Doctor GetDoctorById(int uid) => _doctors.FirstOrDefault(d => d.UserID == uid);

        public Doctor GetDoctorByEmail(string email) =>
            _doctors.FirstOrDefault(d => d.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public Doctor GetDoctorByName(string docName) =>
            _doctors.FirstOrDefault(d => d.FullName.Equals(docName, StringComparison.OrdinalIgnoreCase));

        public void UpdateDoctor(Doctor doctor)
        {
            var existing = _doctors.FirstOrDefault(d => d.UserID == doctor.UserID);
            if (existing != null)
            {
                existing.FullName = doctor.FullName;
                existing.Email = doctor.Email;
                existing.Specialization = doctor.Specialization;
                existing.Salary = doctor.Salary;
                existing.Availability = doctor.Availability;
            }
        }

        public IEnumerable<Doctor> GetDoctorByBrancDep(int bid, int depid)
        {
            return Enumerable.Empty<Doctor>(); // implement later if needed
        }

        public DoctorOutPutDTO GetDoctorData(string? docName, int? Did)
        {
            var doctor = Did != null
                ? GetDoctorById(Did.Value)
                : GetDoctorByName(docName);

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

        public void UpdateDoctorDetails(DoctorUpdateDTO input)
        {
            var doc = GetDoctorById(input.DoctorId);
            if (doc != null)
            {
                doc.Availability = input.Availability;
                doc.Salary = input.Salary;
            }
        }

        public DoctorOutPutDTO GetDoctorDetailsById(int uid)
        {
            var doc = _doctors.FirstOrDefault(d => d.UserID == uid);
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


        public IEnumerable<DoctorOutPutDTO> GetDoctorsByBranchName(string branchName)
        {
            return Enumerable.Empty<DoctorOutPutDTO>(); 
        }

        public IEnumerable<DoctorOutPutDTO> GetDoctorsByDepartmentName(string departmentName)
        {
            return Enumerable.Empty<DoctorOutPutDTO>(); 
        }

        public bool EmailExists(string email)
        {
            return _doctors.Any(d => d.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }


        private List<PatientRecord> _patientRecords = new(); // Add this field if needed

        public void AssignToClinic(int doctorId, int clinicId, int departmentId)
        {
            var doc = _doctors.FirstOrDefault(d => d.UserID == doctorId);
            if (doc != null)
            {
                doc.ClinicId = clinicId;
                doc.DepartmentId = departmentId;
                SaveToFile(); // Save the updated doctor lisT
            }
        }

        public IEnumerable<PatientRecord> GetDoctorPatientRecords(int doctorId)
        {
            return _patientRecords.Where(p => p.DoctorId == doctorId);
        }

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
