using Codeline_HealthCareCenter_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Helpers
{
    public static class DoctorDataHelper
    {
        private static readonly string _filePath = "data/doctors.txt";

        public static void Save(List<Doctor> doctors)
        {
            Directory.CreateDirectory("data");
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                foreach (var d in doctors)
                {
                    writer.WriteLine($"{d.FullName},{d.Email},{d.Password},{d.Specialization},{d.PhoneNumber},{d.Gender},{d.YearsOfExperience},{d.Salary},{d.Availability}");
                }
            }
        }

        public static List<Doctor> Load()
        {
            List<Doctor> doctors = new List<Doctor>();
            if (!File.Exists(_filePath)) return doctors;

            foreach (var line in File.ReadAllLines(_filePath))
            {
                var parts = line.Split(',');
                if (parts.Length == 9)
                {
                    doctors.Add(new Doctor(
                        parts[0],                        // FullName
                        parts[1],                        // Email
                        parts[2],                        // Password
                        parts[3],                        // Specialization
                        parts[4],                        // PhoneNumber
                        parts[5],                        // Gender
                        int.Parse(parts[6]),             // Years of Experience
                        double.Parse(parts[7]),          // Salary
                        parts[8]                         // Availability
                    ));
                }
            }

            return doctors;
        }
    }
}
