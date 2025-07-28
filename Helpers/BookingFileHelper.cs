using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Helpers
{
    public static class BookingFileHelper
    {
        private const string FilePath = "bookings.txt";

        // ✅ Save list of bookings to file
        public static void Save(List<Booking> bookings)
        {
            var lines = bookings.Select(b =>
                $"{b.BookingId}|{b.PatientId}|{b.DoctorId}|{b.ClinicId}|{b.DepartmentId}|{b.AppointmentDate}|{b.AppointmentTime}");
            File.WriteAllLines(FilePath, lines);
        }

        // ✅ Load list of bookings from file
        public static List<Booking> Load()
        {
            if (!File.Exists(FilePath))
                return new List<Booking>();

            return File.ReadAllLines(FilePath)
                .Select(line =>
                {
                    var parts = line.Split('|');
                    return new Booking
                    {
                        BookingId = int.Parse(parts[0]),
                        PatientId = int.Parse(parts[1]),
                        DoctorId = int.Parse(parts[2]),
                        ClinicId = int.Parse(parts[3]),
                        DepartmentId = int.Parse(parts[4]),
                        AppointmentDate = DateTime.Parse(parts[5]),
                        AppointmentTime = TimeSpan.Parse(parts[6])
                    };
                }).ToList();
        }
    }
}
