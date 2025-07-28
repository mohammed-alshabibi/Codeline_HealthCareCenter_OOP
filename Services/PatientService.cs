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
    public class PatientService : IPatientService
    {
        private List<Patient> _patients;
        private readonly string _filePath = "data/patients.txt"; // You can delete this if you move to helper

        public PatientService()
        {
            _patients = PatientDataHelper.Load();
        }

        public void SaveToFile()
        {
            PatientDataHelper.Save(_patients);
        }
        public void AddPatient(PatientInputDTO input)
        {
            var patient = new Patient(
                input.FullName,
                input.Email,
                input.Password,
                input.PhoneNumber,
                input.Gender,
                input.Age,
                input.NationalID
            );

            _patients.Add(patient);
            Console.WriteLine(" Patient added successfully.");
        }

        public IEnumerable<Patient> GetAllPatients() => _patients;

        public Patient GetPatientById(int Pid) =>
            _patients.FirstOrDefault(p => p.UserID == Pid);

        public Patient GetPatientByName(string name) =>
            _patients.FirstOrDefault(p => p.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));

        public void UpdatePatientDetails(int uid, string phone)
        {
            var p = _patients.FirstOrDefault(p => p.UserID == uid);
            if (p != null)
                p.PhoneNumber = phone;
        }


        public PatienoutputDTO GetPatientData(string? name, int? Pid)
        {
            var p = Pid != null
                ? GetPatientById(Pid.Value)
                : GetPatientByName(name);

            if (p == null) return null;

            return new PatienoutputDTO
            {
                FullName = p.FullName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                Gender = p.Gender,
                Age = p.Age,
                NationalID = p.NationalID
            };
        }
    }
}

