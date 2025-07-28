using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.DTO_s // Codeline_HealthCareCenter_OOP.DTO_s
{
    public class BookingOutputDTO // Codeline_HealthCareCenter_OOP.DTO_s
    {
        public int BookingId { get; set; } // Unique identifier for the booking
        public string PatientName { get; set; } // Unique identifier for the patient
        public string DoctorName { get; set; } // Unique identifier for the doctor
        public string ClinicName { get; set; } // Unique identifier for the clinic
        public DateTime AppointmentDate { get; set; } // Date of the appointment
        public TimeSpan AppointmentTime { get; set; } // Time of the appointment
    }

}
