using Codeline_HealthCareCenter_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Helpers
{
    public static class PatientDataHelper
    {
        private static string _filePath = "data/patients.txt";

        public static void Save(List<Patient> patients)
        {
            Directory.CreateDirectory("data");
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                foreach (var p in patients)
                {
                    writer.WriteLine($"{p.FullName},{p.Email},{p.Password},{p.PhoneNumber},{p.Gender},{p.Age},{p.NationalID},{p.PatientID}");
                }
            }
        }

        public static List<Patient> Load()
        {
            List<Patient> patients = new List<Patient>();
            if (!File.Exists(_filePath)) return patients;

            foreach (var line in File.ReadAllLines(_filePath))
            {
                var parts = line.Split(',');
                if (parts.Length == 8)
                {
                    patients.Add(new Patient(
                        parts[0], // FullName
                        parts[1], // Email
                        parts[2], // Password
                        int.Parse(parts[3]), // PhoneNumber
                        parts[4], // Gender
                        int.Parse(parts[5]), // Age
                        int.Parse(parts[6]), // NationalID
                        int.Parse(parts[7]) // Id_Patient
                    ));
                }
            }

            return patients;
        }

    }
}
