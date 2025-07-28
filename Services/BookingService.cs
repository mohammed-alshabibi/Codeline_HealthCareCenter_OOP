using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class BookingService : IBookingService // IBookingService
    {
        private List<Booking> bookings = new List<Booking>(); // List<Booking>
        private int bookingCounter = 1; // Counter for booking IDs

        public void BookAppointment(BookingInputDTO input, int patientId) // BookingInputDTO input, int patientId)
        {
            var booking = new Booking // Booking
            {
                BookingId = bookingCounter++, // Increment booking ID for each new booking
                PatientId = patientId, // PatientId
                ClinicId = input.ClinicId ?? 0, // ClinicId
                DepartmentId = input.DepartmentId ?? 0, // DepartmentId
                DoctorId = input.DoctorId ?? 0, 
                AppointmentDate = input.AppointmentDate,
                AppointmentTime = input.AppointmentTime
            };
            bookings.Add(booking);
        }


        /// Cancels an appointment for a patient
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

        /// Deletes appointments based on the input criteria
        public void DeleteAppointments(BookingInputDTO input)
        {
            bookings.RemoveAll(b =>
                b.ClinicId == input.ClinicId &&
                b.AppointmentDate == input.AppointmentDate &&
                b.AppointmentTime == input.AppointmentTime);
        }


        /// Retrieves all bookings and maps them to BookingOutputDTO
        public IEnumerable<BookingOutputDTO> GetAllBooking()
        {
            return bookings.Select(MapToOutputDTO);
        }

        public IEnumerable<BookingInputDTO> GetAvailableAppointmentsBy(int? clinicId, int? departmentId)
        {
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


        /// Retrieves booked appointments for a specific patient
        public IEnumerable<BookingOutputDTO> GetBookedAppointments(int patientId)
        {
            return bookings
                .Where(b => b.PatientId == patientId)
                .Select(MapToOutputDTO);
        }
        // /// Retrieves a booking by its ID
        public Booking GetBookingById(int bookingId)
        {
            return bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }

        /// Retrieves bookings for a specific clinic and date
        public IEnumerable<Booking> GetBookingsByClinicAndDate(int clinicId, DateTime date)
        {
            return bookings.Where(b =>
                b.ClinicId == clinicId &&
                b.AppointmentDate.Date == date.Date);
        }
        // /// Retrieves scheduled appointments for a specific clinic and date
        public IEnumerable<Booking> ScheduledAppointments(int clinicId, DateTime appointmentDate)
        {
            return bookings.Where(b =>
                b.ClinicId == clinicId &&
                b.AppointmentDate.Date == appointmentDate.Date);
        }

        /// Updates a booked appointment for a patient
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

        /// Maps a Booking object to a BookingOutputDTO
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
