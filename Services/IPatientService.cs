using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(int Pid);
        void UpdatePatientDetails(int UID, string phoneNumber);
        void AddPatient(PatientInputDTO patientInput);
        Patient GetPatientByName(string PatientName);
        PatienoutputDTO AuthenticatePatient(PatientInputDTO dto);
    }
}