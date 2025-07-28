using System.Collections.Generic;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IClinicService
    {
        void AddClinic(ClinicInputDTO input);
        void UpdateClinic(int clinicId, ClinicInputDTO input);
        bool DeleteClinic(int clinicId);
        Clinic GetClinicById(int clinicId);
        IEnumerable<ClinicOutputDTO> GetAllClinics();
    }
}
