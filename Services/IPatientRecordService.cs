using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IPatientRecordService
    {
        IEnumerable<PatientRecordOutput> GetAllRecords();
        void CreateRecord(PatientRecordInputDTO record, int doctorId);
        void UpdateRecord(int rid, string? treatment, string? inspection, int doctorId);
        void DeleteRecord(int rid);
        public IEnumerable<PatientRecordOutput> GetRecords(int patientId);
    }
}