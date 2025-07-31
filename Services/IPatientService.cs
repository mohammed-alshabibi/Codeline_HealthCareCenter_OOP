using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IPatientService
    {
        IEnumerable<BookingInputDTO> GetAvailableAppointments(int clinicId, int departmentId);
       

        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(int Pid);
        void UpdatePatientDetails(int UID, int phoneNumber);
        Patient GetPatientByName(string PatientName);
        PatienoutputDTO AuthenticatePatient(PatientInputDTO dto);
        void AddPatient(PatientInputDTO dto);

    }
}