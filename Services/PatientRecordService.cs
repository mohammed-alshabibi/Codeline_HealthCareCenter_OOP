using System;
using System.Collections.Generic;
using System.Linq;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class PatientRecordService : IPatientRecordService
    {
        private List<PatientRecord> records = new List<PatientRecord>();
        private int recordCounter = 1;
        // Load file to initialize the PatientRecordService
        public PatientRecordService()
        {
            records = PatientRecordFileHelper.Load(); //  Load from file
            if (records.Count > 0)
                recordCounter = records.Max(r => r.RecordId) + 1;
        }
        // Method to add a new patient record
        public void AddRecord(PatientRecordInputDTO input)
        {
            var record = new PatientRecord
            {
                RecordId = recordCounter++,
                PatientId = input.PatientId,
                PatientName = input.PatientName,
                Diagnosis = input.Diagnosis,
                Treatment = input.Treatment,
                VisitDate = input.VisitDate,
                Notes = input.Notes
            };
            records.Add(record);
            PatientRecordFileHelper.Save(records);
            Console.WriteLine("Record added successfully!");
        }
        // Method to update an existing patient record
        public void UpdateRecord(int recordId, PatientRecordInputDTO input)
        {
            var record = records.FirstOrDefault(r => r.RecordId == recordId);
            if (record != null)
            {
                record.PatientId = input.PatientId;
                record.PatientName = input.PatientName;
                record.Diagnosis = input.Diagnosis;
                record.Treatment = input.Treatment;
                record.VisitDate = input.VisitDate;
                PatientRecordFileHelper.Save(records); //  Save to file
                Console.WriteLine("Record updated successfully!");
            }
          
        }
        // Method to delete a patient record by ID
        public bool DeleteRecord(int recordId)
        {
            var record = records.FirstOrDefault(r => r.RecordId == recordId);
            if (record != null)
            {
                records.Remove(record);
                PatientRecordFileHelper.Save(records); //  Save to file
                return true;
            }
            return false;
        }
        // Method to get a patient record by its ID
        public PatientRecord? GetRecordById(int recordId)
        {
            return records.FirstOrDefault(r => r.RecordId == recordId);
        }
        // Method to retrieve all patient records
        public IEnumerable<PatientRecordOutputDTO> GetAllRecords()
        {
            return records.Select(r => MapToOutputDTO(r));
        }
        // Method to get patient records by patient name
        public IEnumerable<PatientRecordOutputDTO> GetRecordsByPatientName(string patientName)
        {
            return records
                .Where(r => r.PatientName.ToLower().Contains(patientName.ToLower()))
                .Select(r => MapToOutputDTO(r));
        }
        // Method to get patient records within a specific date range
        public IEnumerable<PatientRecordOutputDTO> GetRecordsByDateRange(DateTime start, DateTime end)
        {
            return records
                .Where(r => r.VisitDate.Date >= start.Date && r.VisitDate.Date <= end.Date)
                .Select(r => MapToOutputDTO(r));
        }
        // Method to map PatientRecord to PatientRecordOutputDTO
        private PatientRecordOutputDTO MapToOutputDTO(PatientRecord record)
        {
            return new PatientRecordOutputDTO
            {
                RecordId = record.RecordId,
                PatientName = record.PatientName,
                Diagnosis = record.Diagnosis,
                Treatment = record.Treatment,
                VisitDate = record.VisitDate
            };
        }
    }
}
