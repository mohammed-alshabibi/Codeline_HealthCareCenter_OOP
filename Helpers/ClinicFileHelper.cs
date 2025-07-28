using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Helpers
{
    public static class ClinicFileHelper
    {
        private const string FilePath = "clinics.txt";

        // Save clinics to file
        public static void Save(List<Clinic> clinics)
        {
            var lines = clinics.Select(c =>
                $"{c.ClinicId}|{c.ClinicName}|{c.Department}|{c.Location}");
            File.WriteAllLines(FilePath, lines);
        }

        // Load clinics from file
        public static List<Clinic> Load()
        {
            if (!File.Exists(FilePath))
                return new List<Clinic>();

            return File.ReadAllLines(FilePath)
                .Select(line =>
                {
                    var parts = line.Split('|');
                    return new Clinic
                    {
                        ClinicId = int.Parse(parts[0]),
                        ClinicName = parts[1],
                        Department = parts[2],
                        Location = parts[3]
                    };
                }).ToList();
        }
    }
}
