using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Services;


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

    public class BookingService : IBookingService
    {
        private List<Booking> bookings = new List<Booking>();
        private int bookingCounter = 1;

        public void BookAppointment(BookingInputDTO input, int patientId)
        {
            var booking = new Booking
            {
                BookingId = bookingCounter++,
                PatientId = patientId,
                ClinicId = input.ClinicId ?? 0,
                DepartmentId = input.DepartmentId ?? 0,
                DoctorId = input.DoctorId ?? 0,
                AppointmentDate = input.AppointmentDate,
                AppointmentTime = input.AppointmentTime
            };
            bookings.Add(booking);
        }

        public void CancelAppointment(BookingInputDTO input, int patientId)
        {
            var booking = bookings.FirstOrDefault(b =>
                b.PatientId == patientId &&
                b.AppointmentDate == input.AppointmentDate &&
                b.AppointmentTime == input.AppointmentTime);
            if (booking != null)
            {
                bookings.Remove(booking);
            }
        }

        public void DeleteAppointments(BookingInputDTO input)
        {
            bookings.RemoveAll(b =>
                b.ClinicId == input.ClinicId &&
                b.AppointmentDate == input.AppointmentDate &&
                b.AppointmentTime == input.AppointmentTime);
        }

        public IEnumerable<BookingOutputDTO> GetAllBooking()
        {
            return bookings.Select(MapToOutputDTO);
        }

        public IEnumerable<BookingInputDTO> GetAvailableAppointmentsBy(int? clinicId, int? departmentId)
        {
            // Fake open times from 9AM to 4PM for today
            var usedTimes = bookings
                .Where(b => b.ClinicId == clinicId && b.DepartmentId == departmentId)
                .Select(b => b.AppointmentTime)
                .ToHashSet();

            List<BookingInputDTO> available = new List<BookingInputDTO>();
            for (int i = 9; i <= 16; i++)
            {
                var time = TimeSpan.FromHours(i);
                if (!usedTimes.Contains(time))
                {
                    available.Add(new BookingInputDTO
                    {
                        ClinicId = clinicId,
                        DepartmentId = departmentId,
                        AppointmentDate = DateTime.Today,
                        AppointmentTime = time
                    });
                }
            }
            return available;
        }

        public IEnumerable<BookingOutputDTO> GetBookedAppointments(int patientId)
        {
            return bookings
                .Where(b => b.PatientId == patientId)
                .Select(MapToOutputDTO);
        }

        public Booking GetBookingById(int bookingId)
        {
            return bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }

        public IEnumerable<Booking> GetBookingsByClinicAndDate(int clinicId, DateTime date)
        {
            return bookings.Where(b => b.ClinicId == clinicId && b.AppointmentDate.Date == date.Date);
        }

        public IEnumerable<Booking> ScheduledAppointments(int clinicId, DateTime appointmentDate)
        {
            return bookings.Where(b => b.ClinicId == clinicId && b.AppointmentDate.Date == appointmentDate.Date);
        }

        public void UpdateBookedAppointment(BookingInputDTO previous, BookingInputDTO updated, int patientId)
        {
            var booking = bookings.FirstOrDefault(b =>
                b.PatientId == patientId &&
                b.AppointmentDate == previous.AppointmentDate &&
                b.AppointmentTime == previous.AppointmentTime);

            if (booking != null)
            {
                booking.ClinicId = updated.ClinicId ?? booking.ClinicId;
                booking.DepartmentId = updated.DepartmentId ?? booking.DepartmentId;
                booking.DoctorId = updated.DoctorId ?? booking.DoctorId;
                booking.AppointmentDate = updated.AppointmentDate;
                booking.AppointmentTime = updated.AppointmentTime;
            }
        }

        private BookingOutputDTO MapToOutputDTO(Booking b)
        {
            return new BookingOutputDTO
            {
                BookingId = b.BookingId,
                PatientName = $"Patient #{b.PatientId}",
                DoctorName = $"Doctor #{b.DoctorId}",
                ClinicName = $"Clinic #{b.ClinicId}",
                AppointmentDate = b.AppointmentDate,
                AppointmentTime = b.AppointmentTime
            };
        }
    }
}
