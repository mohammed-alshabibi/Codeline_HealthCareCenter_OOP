using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IDoctorService
    {

        void AssignToClinic(int doctorId, int clinicId, int departmentId);
        IEnumerable<PatientRecord> GetDoctorPatientRecords(int doctorId);
        void AddOrUpdatePatientRecord(int doctorId, PatientRecord record);

        void AddDoctor(DoctorInput input);
        bool EmailExists(string email);
        IEnumerable<Doctor> GetAllDoctors();
        IEnumerable<Doctor> GetDoctorByBrancDep(int bid, int depid);
        Doctor GetDoctorByEmail(string email);
        Doctor GetDoctorById(int uid);
        Doctor GetDoctorByName(string docName);
        DoctorOutPutDTO GetDoctorData(string? docName, int? Did);
        IEnumerable<DoctorOutPutDTO> GetDoctorsByBranchName(string branchName);
        IEnumerable<DoctorOutPutDTO> GetDoctorsByDepartmentName(string departmentName);
        void UpdateDoctor(Doctor doctor);
        void UpdateDoctorDetails(DoctorUpdateDTO input);
        DoctorOutPutDTO GetDoctorDetailsById(int uid);

    }
}