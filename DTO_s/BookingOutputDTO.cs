using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.DTO_s
{
    public class BookingOutputDTO
    {
        public int BookingId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string ClinicName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
    }

}
