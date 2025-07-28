using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Helpers
{
    public static class PatientRecordFileHelper
    {
        private const string FilePath = "patients.txt";

        //  Save patient records to file
        public static void Save(List<PatientRecord> records)
        {
            var lines = records.Select(r =>
                $"{r.RecordId}|{r.PatientId}|{r.PatientName}|{r.Diagnosis}|{r.Treatment}|{r.VisitDate}");
            File.WriteAllLines(FilePath, lines);
        }

        //  Load patient records from file
        public static List<PatientRecord> Load()
        {
            if (!File.Exists(FilePath))
                return new List<PatientRecord>();

            return File.ReadAllLines(FilePath)
                .Select(line =>
                {
                    var parts = line.Split('|');
                    return new PatientRecord
                    {
                        RecordId = int.Parse(parts[0]),
                        PatientId = int.Parse(parts[1]),
                        PatientName = parts[2],
                        Diagnosis = parts[3],
                        Treatment = parts[4],
                        VisitDate = DateTime.Parse(parts[5])
                    };
                }).ToList();
        }
    }
}