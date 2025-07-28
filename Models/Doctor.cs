

namespace Codeline_HealthCareCenter_OOP.Models
{
    public class Doctor : User
    {
        private string _doctorId;
        private string _specialization;
        private string _phoneNumber;
        private string _gender;
        private int _experience;
        private double _salary;
        private string _availability;

        public string DoctorID
        {
            get => _doctorId;
            private set => _doctorId = value;
        }

        public string Specialization
        {
            get => _specialization;
            set => _specialization = value;
        }

        public string PhoneNumber
        {
            get; private set;
        }

        public string Gender
        {
            get => _gender;
            set => _gender = value;
        }

        internal int YearsOfExperience
        {
            get => _experience;
            set => _experience = value > 0 ? value : 0;
        }

        public double Salary
        {
            get => _salary;
            set => _salary = value >= 0 ? value : 0;
        }

        public string Availability
        {
            get => _availability;
            set => _availability = value;
        }

        public Doctor(string fullName, string email, string password, string specialization, string phoneNumber,
                      string gender, int experience, double salary, string availability)
            : base(fullName, email, password, "Doctor")
        {
            DoctorID = $"DOC{UserID:D4}";
            Specialization = specialization;
            PhoneNumber = phoneNumber;
            Gender = gender;
            YearsOfExperience = experience;
            Salary = salary;
            Availability = availability;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"[DoctorID: {DoctorID}] | {Specialization} | Exp: {YearsOfExperience} yrs | Phone: {PhoneNumber}");
        }

    }
}
