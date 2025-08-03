using System.Collections.Generic;
using System.Linq;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;

namespace Codeline_HealthCareCenter_OOP.Services
{
    /// Interface for Clinic Service
    public class ClinicService : IClinicService // Implements the IClinicService interface
    {
        private List<Clinic> clinics = new List<Clinic>();
        private int clinicCounter = 1;
        // Load File to initialize the ClinicService
        public ClinicService()
        {
            clinics = ClinicFileHelper.Load();
            if (clinics.Count > 0)
            {
                clinicCounter = clinics.Max(c => c.ClinicId) + 1; // Start from the next ID
            }
        }
        //  Adds a new clinic based on the input DTO
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
            ClinicFileHelper.Save(clinics);
            Console.WriteLine("Clinc Added...");
        }
        // Updates an existing clinic based on the clinicId
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
        // Deletes a clinic based on the clinicId
        public bool DeleteClinic(int clinicId)
        {
            var clinic = clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            if (clinic != null)
            {
                clinics.Remove(clinic);
                ClinicFileHelper.Save(clinics);
                return true;
            }
            return false;


        }
        //  Retrieves a clinic by its ID
        public Clinic? GetClinicById(int clinicId)
        {
            return clinics.FirstOrDefault(c => c.ClinicId == clinicId);
        }
        // Retrieves all clinics and maps them to ClinicOutputDTO
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
