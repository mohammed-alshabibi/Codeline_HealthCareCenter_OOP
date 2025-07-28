using System;
using System.Collections.Generic;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IPatientRecordService
    {
        void AddRecord(PatientRecordInputDTO input);
        void UpdateRecord(int recordId, PatientRecordInputDTO input);
        bool DeleteRecord(int recordId);
        PatientRecord GetRecordById(int recordId);
        IEnumerable<PatientRecordOutputDTO> GetAllRecords();
        IEnumerable<PatientRecordOutputDTO> GetRecordsByPatientName(string patientName);
        IEnumerable<PatientRecordOutputDTO> GetRecordsByDateRange(DateTime start, DateTime end);
    }
}
