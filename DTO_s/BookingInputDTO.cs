using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.DTO_s
{
    public class BookingInputDTO //  Represents the input data for booking an appointment
    {
        public int? ClinicId { get; set; } // Unique identifier for the clinic
        public int? DepartmentId { get; set; } // Unique identifier for the department
        public int? DoctorId { get; set; } //  Unique identifier for the doctor
        public DateTime AppointmentDate { get; set; } // Date of the appointment
        public TimeSpan AppointmentTime { get; set; } // Time of the appointment
    }
}
