using System;
using System.Collections.Generic;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IBookingService
    {
        void BookAppointment(BookingInputDTO input, int patientId); // Method to book an appointment
        void CancelAppointment(BookingInputDTO input, int patientId); // Method to cancel an appointment
        void DeleteAppointments(BookingInputDTO input); // Method to delete appointments
        IEnumerable<BookingOutputDTO> GetAllBooking(); // Method to retrieve all bookings
        IEnumerable<BookingInputDTO> GetAvailableAppointmentsBy(int? clinicId, int? departmentId); // Method to get available appointments by clinic and department
        IEnumerable<BookingOutputDTO> GetBookedAppointments(int patientId); // Method to get booked appointments for a patient
        Booking? GetBookingById(int bookingId); // Method to get a booking by its ID
        IEnumerable<Booking> GetBookingsByClinicAndDate(int clinicId, DateTime date); // Method to get bookings by clinic and date
        IEnumerable<Booking> ScheduledAppointments(int clinicId, DateTime date); // Method to get scheduled appointments for a clinic on a specific date
        void UpdateBookedAppointment(BookingInputDTO previous, BookingInputDTO updated, int patientId); // Method to update a booked appointment
    }
}
