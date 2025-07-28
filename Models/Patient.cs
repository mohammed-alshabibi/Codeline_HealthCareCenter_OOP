using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Models
{
    public class Patient : User
    {
        private string _phoneNumber;
        private string _gender;
        private int _age;
        private string _nationalId;

        public string PhoneNumber
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

        public string NationalID
        {
            get => _nationalId;
            internal set => _nationalId = value;
        }

        public Patient(string fullName, string email, string password, string phoneNumber, string gender, int age, string nationalId)
            : base(fullName, email, password, "Patient")
        {
            PhoneNumber = phoneNumber;
            Gender = gender;
            Age = age;
            NationalID = nationalId;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Phone: {PhoneNumber} | Age: {Age} | National ID: {NationalID}");
        }

    }
}
