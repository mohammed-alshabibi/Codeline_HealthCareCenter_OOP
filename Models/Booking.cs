using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;

namespace Codeline_HealthCareCenter_OOP.Models // Codeline_HealthCareCenter_OOP.Models
{
    public class Booking // Represents a booking in the healthcare system
    {
        public int BookingId { get; set; } // Unique identifier for the booking
        public int PatientId { get; set; } // Unique identifier for the patient
        public int DoctorId { get; set; } // Unique identifier for the doctor
        public int ClinicId { get; set; } // Unique identifier for the clinic
        public int DepartmentId { get; set; } // Unique identifier for the department
        public DateTime AppointmentDate { get; set; }   // Date of the appointment
        public TimeSpan AppointmentTime { get; set; }   // Time of the appointment
    }
}
