using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.DTO_s
{
    public class DoctorOutPutDTO
    {
        public string DoctorID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string Availability { get; set; }
        public double Salary { get; set; }
    }
}
