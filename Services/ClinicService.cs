using System.Collections.Generic;
using System.Linq;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class ClinicService : IClinicService
    {
        private List<Clinic> clinics = new List<Clinic>();
        private int clinicCounter = 1;

        public void AddClinic(ClinicInputDTO input)
        {
            var clinic = new Clinic
            {
                ClinicId = clinicCounter++,
                ClinicName = input.ClinicName,
                Department = input.Department,
                Location = input.Location
            };
            clinics.Add(clinic);
        }

        public void UpdateClinic(int clinicId, ClinicInputDTO input)
        {
            var clinic = clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            if (clinic != null)
            {
                clinic.ClinicName = input.ClinicName;
                clinic.Department = input.Department;
                clinic.Location = input.Location;
            }
        }

        public bool DeleteClinic(int clinicId)
        {
            var clinic = clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            if (clinic != null)
            {
                clinics.Remove(clinic);
                return true;
            }
            return false;
        }

        public Clinic GetClinicById(int clinicId)
        {
            return clinics.FirstOrDefault(c => c.ClinicId == clinicId);
        }

        public IEnumerable<ClinicOutputDTO> GetAllClinics()
        {
            return clinics.Select(c => new ClinicOutputDTO
            {
                ClinicId = c.ClinicId,
                ClinicName = c.ClinicName,
                Department = c.Department,
                Location = c.Location
            });
        }
    }
}
