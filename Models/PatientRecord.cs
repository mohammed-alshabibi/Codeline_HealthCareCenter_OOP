using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Models
{
    public class PatientRecord
    {
        public int RecordId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
