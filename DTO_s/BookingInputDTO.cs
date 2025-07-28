using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.DTO_s
{
    public class BookingInputDTO
    {
        public int? ClinicId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
    }
}
