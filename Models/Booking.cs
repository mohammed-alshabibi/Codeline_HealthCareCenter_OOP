using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.IO;

namespace HospitalSystem
{
    public class Booking
    {
        // Fields (Encapsulation)
        private string bookingId;
        private string patientId;
        private string doctorId;
        private DateTime bookingDate;
        private string clinicName;

        // Properties
        public string BookingId
        {
            get { return bookingId; }
            set { bookingId = value; }
        }

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public string DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; }
        }

        public DateTime BookingDate
        {
            get { return bookingDate; }
            set { bookingDate = value; }
        }

        public string ClinicName
        {
            get { return clinicName; }
            set { clinicName = value; }
        }

        // Constructor
        public Booking(string bookingId, string patientId, string doctorId, DateTime bookingDate, string clinicName)
        {
            BookingId = bookingId;
            PatientId = patientId;
            DoctorId = doctorId;
            BookingDate = bookingDate;
            ClinicName = clinicName;
        }

        // Display Method
        public override string ToString()
        {
            return $"Booking ID: {BookingId}, Patient ID: {PatientId}, Doctor ID: {DoctorId}, Date: {BookingDate}, Clinic: {ClinicName}";
        }

        // Save to file
        public void SaveToFile(string filePath)
        {
            string line = $"{BookingId}|{PatientId}|{DoctorId}|{BookingDate}|{ClinicName}";
            File.AppendAllText(filePath, line + Environment.NewLine);
        }

        // Load from file (example for one booking line)
        public static Booking LoadFromLine(string line)
        {
            string[] parts = line.Split('|');
            if (parts.Length != 5)
                throw new FormatException("Invalid booking record format.");

            return new Booking(
                parts[0],  // BookingId
                parts[1],  // PatientId
                parts[2],  // DoctorId
                DateTime.Parse(parts[3]), // BookingDate
                parts[4]   // ClinicName
            );
        }
    }
}
