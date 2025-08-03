using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IDoctorService
    {

        void AssignToClinic(int doctorId, int clinicId, int departmentId); // Method to assign a doctor to a clinic and department
        IEnumerable<PatientRecord> GetDoctorPatientRecords(int doctorId); // Method to get all patient records for a specific doctor
        void AddOrUpdatePatientRecord(int doctorId, PatientRecord record); // Method to add or update a patient record for a doctor
        void AddDoctor(DoctorInput input); // Method to add a new doctor
        bool EmailExists(string email); // Method to check if an email already exists in the doctor list
        IEnumerable<Doctor> GetAllDoctors(); // Method to retrieve all doctors
        IEnumerable<Doctor> GetDoctorByBrancDep(int bid, int depid); // Method to get doctors by branch and department IDs
        Doctor GetDoctorByEmail(string email); // Method to get a doctor by their email
        Doctor GetDoctorById(int uid); // Method to get a doctor by their unique ID
        Doctor GetDoctorByName(string docName); // Method to get a doctor by their name
        DoctorOutPutDTO GetDoctorData(string? docName, int? Did); // Method to get doctor data by name or department ID
        IEnumerable<DoctorOutPutDTO> GetDoctorsByBranchName(string branchName); // Method to get doctors by branch name
        IEnumerable<DoctorOutPutDTO> GetDoctorsByDepartmentName(string departmentName); // Method to get doctors by department name
        void UpdateDoctor(Doctor doctor); // Method to update a doctor's details
        void UpdateDoctorDetails(DoctorUpdateDTO input); // Method to update a doctor's details based on input DTO
        DoctorOutPutDTO GetDoctorDetailsById(int uid); // Method to get doctor details by ID

    }
}