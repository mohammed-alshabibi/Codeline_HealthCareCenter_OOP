using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Models
{
    public class Patient : User
    {
        private int _phoneNumber;
        private string _gender = string.Empty;
        private int _age;
        private int _nationalId;
        private static int _patientCounter = 1;
        private static int _idPatient;

        public int PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        public string Gender
        {
            get => _gender;
            set => _gender = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value > 0 ? value : 0;
        }

        public int NationalID
        {
            get => _nationalId;
            internal set => _nationalId = value;
        }
        public int PatientID
        {
            get => _idPatient;
            private set => _idPatient = value > 0 ? value : _patientCounter++;
        }

        public Patient(string fullName, string email, string password, int phoneNumber, string gender, int age, int nationalId, int Id_Patient)
            : base(fullName, email, password, "Patient")
        {
            PhoneNumber = phoneNumber;
            Gender = gender;
            Age = age;
            NationalID = nationalId;
            PatientID = Id_Patient;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Phone: {PhoneNumber} | Age: {Age} | National ID: {NationalID}");
        }

    }
}
