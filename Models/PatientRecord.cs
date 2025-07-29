using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Codeline_HealthCareCenter_OOP.Models
{
    public class PatientRecord // Represents a patient's medical record
    {
        public int RecordId { get; set; }        // Unique identifier for the patient record
        public int PatientId { get; set; }       // Unique identifier for the patient associated with the record
        public string PatientName { get; set; }  // Name of the patient associated with the record
        public string Diagnosis { get; set; }    // Diagnosis given to the patient in the record
        public string Treatment { get; set; }    // Treatment prescribed for the patient in the record
        public DateTime VisitDate { get; set; }  // Date of the patient's visit recorded in the record

        public string Notes { get; set; } // Add this line

        public int DoctorId { get; set; }        // Doctor assigned to the record
    }
}
