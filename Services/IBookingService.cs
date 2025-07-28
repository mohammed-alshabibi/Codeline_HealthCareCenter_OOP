using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IBookingService
    {
        public interface IBookingService
        {
            void BookAppointment(BookingInputDTO input, int patientId); // BookingInputDTO input, int patientId);
            void CancelAppointment(BookingInputDTO input, int patientId); // BookingInputDTO input, int patientId);
            void DeleteAppointments(BookingInputDTO input); // BookingInputDTO input);

            IEnumerable<BookingOutputDTO> GetAllBooking(); // IEnumerable<BookingOutputDTO> GetAllBooking();
            IEnumerable<BookingInputDTO> GetAvailableAppointmentsBy(int? clinicId, int? departmentId); // int? clinicId, int? departmentId);
            IEnumerable<BookingOutputDTO> GetBookedAppointments(int patientId); // IEnumerable<BookingOutputDTO> GetBookedAppointments(int patientId);
            Booking GetBookingById(int bookingId); // Booking GetBookingById(int bookingId);
            IEnumerable<Booking> GetBookingsByClinicAndDate(int clinicId, DateTime date); // int clinicId, DateTime date);
            IEnumerable<Booking> ScheduledAppointments(int clinicId, DateTime date); // int clinicId, DateTime date);

            void UpdateBookedAppointment(BookingInputDTO previous, BookingInputDTO updated, int patientId); // BookingInputDTO previous, BookingInputDTO updated, int patientId);
        }



    }

}