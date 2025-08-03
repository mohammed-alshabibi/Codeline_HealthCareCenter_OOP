using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IPatientService
    {
        IEnumerable<BookingInputDTO> GetAvailableAppointments(int clinicId, int departmentId); // Method to get available appointments by clinic and department
        IEnumerable<Patient> GetAllPatients(); // Method to retrieve all patients
        Patient GetPatientById(int Pid); // Method to get a patient by their unique ID
        void UpdatePatientDetails(int UID, int phoneNumber); // Method to update a patient's details
        Patient GetPatientByName(string PatientName); // Method to get a patient by their name
        PatienoutputDTO AuthenticatePatient(PatientInputDTO dto); // Method to authenticate a patient
        void AddPatient(PatientInputDTO dto); // Method to add a new patient

    }
}