using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Codeline_HealthCareCenter_OOP.DTO_s
{
    public class PatientRecordInputDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
