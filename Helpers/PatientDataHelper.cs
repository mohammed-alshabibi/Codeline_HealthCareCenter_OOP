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
                    writer.WriteLine($"{p.UserID},{p.FullName},{p.NationalID},{p.Email},{p.PhoneNumber},{p.Password}");
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
                if (parts.Length == 6)
                {
                    patients.Add(new Patient
                    {
                        Id = parts[0],
                        FullName = parts[1],
                        NationalId = parts[2],
                        Email = parts[3],
                        PhoneNumber = parts[4],
                        Password = parts[5]
                    });
                }
            }
            return patients;
        }
    }
