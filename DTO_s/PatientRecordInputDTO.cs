using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Codeline_HealthCareCenter_OOP.DTO_s
{
    public class PatientRecordInputDTO // Represents the input data for a patient record
    {
        public int PatientId { get; set; } // Unique identifier for the patient
        public string PatientName { get; set; } // Name of the patient 
        public string Diagnosis { get; set; } // Diagnosis given to the patient
        public string Treatment { get; set; } // Treatment prescribed for the patient
        public DateTime VisitDate { get; set; } // Date of the patient's visit
    }
}
