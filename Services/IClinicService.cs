using System.Collections.Generic;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IClinicService
    {
        void AddClinic(ClinicInputDTO input); // Method to add a new clinic
        void UpdateClinic(int clinicId, ClinicInputDTO input); // Method to update an existing clinic
        bool DeleteClinic(int clinicId); // Method to delete a clinic
        Clinic GetClinicById(int clinicId); // Method to get a clinic by its ID
        IEnumerable<ClinicOutputDTO> GetAllClinics(); // Method to retrieve all clinics
    }
}
