using System;
using System.Collections.Generic;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.DTO_s;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IPatientRecordService
    {
        void AddRecord(PatientRecordInputDTO input); // Method to add a new patient record
        void UpdateRecord(int recordId, PatientRecordInputDTO input); // Method to update an existing patient record
        bool DeleteRecord(int recordId); // Method to delete a patient record
        PatientRecord GetRecordById(int recordId); // Method to get a patient record by its ID
        IEnumerable<PatientRecordOutputDTO> GetAllRecords(); // Method to retrieve all patient records
        IEnumerable<PatientRecordOutputDTO> GetRecordsByPatientName(string patientName); // Method to get patient records by patient name
        IEnumerable<PatientRecordOutputDTO> GetRecordsByDateRange(DateTime start, DateTime end); // Method to get patient records within a specific date range
    }
}
