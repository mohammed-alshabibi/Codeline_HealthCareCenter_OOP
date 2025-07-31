using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.DTO_s
{
    public class PatientRecordOutputDTO // Represents the output DTO for patient records
    {
        // Properties to hold patient record details
        public int PatientId { get; set; } // Unique identifier for the patient
        public int RecordId { get; set; } // Unique identifier for the patient record
        public string PatientName { get; set; } // Name of the patient associated with the record
        public string Diagnosis { get; set; } // Diagnosis given to the patient in the record
        public string Treatment { get; set; } // Treatment prescribed for the patient in the record
        public DateTime VisitDate { get; set; } // Date of the patient's visit recorded in the record
    }
}
