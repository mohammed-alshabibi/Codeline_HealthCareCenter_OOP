using System;
using System.Collections.Generic;
using System.Linq;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class PatientRecordService : IPatientRecordService
    {
        private List<PatientRecord> records = new List<PatientRecord>();
        private int recordCounter = 1;

        public void AddRecord(PatientRecordInputDTO input)
        {
            var record = new PatientRecord
            {
                RecordId = recordCounter++,
                PatientId = input.PatientId,
                PatientName = input.PatientName,
                Diagnosis = input.Diagnosis,
                Treatment = input.Treatment,
                VisitDate = input.VisitDate
            };
            records.Add(record);
        }

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
            }
        }

        public bool DeleteRecord(int recordId)
        {
            var record = records.FirstOrDefault(r => r.RecordId == recordId);
            if (record != null)
            {
                records.Remove(record);
                return true;
            }
            return false;
        }

        public PatientRecord GetRecordById(int recordId)
        {
            return records.FirstOrDefault(r => r.RecordId == recordId);
        }

        public IEnumerable<PatientRecordOutputDTO> GetAllRecords()
        {
            return records.Select(r => MapToOutputDTO(r));
        }

        public IEnumerable<PatientRecordOutputDTO> GetRecordsByPatientName(string patientName)
        {
            return records
                .Where(r => r.PatientName.ToLower().Contains(patientName.ToLower()))
                .Select(r => MapToOutputDTO(r));
        }

        public IEnumerable<PatientRecordOutputDTO> GetRecordsByDateRange(DateTime start, DateTime end)
        {
            return records
                .Where(r => r.VisitDate.Date >= start.Date && r.VisitDate.Date <= end.Date)
                .Select(r => MapToOutputDTO(r));
        }

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
