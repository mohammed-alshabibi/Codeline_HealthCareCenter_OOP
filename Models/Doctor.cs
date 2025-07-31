

using Codeline_HealthCareCenter_OOP.Models;

public class Doctor : User
{
    // Existing fields
    private string doctorId;
    private string specialization;
    private string phoneNumber;
    private string gender;
    private int experience;
    private double salary;
    private string availability;

    //  Add these two properties
    public int ClinicId { get; set; }
    public int DepartmentId { get; set; }

    public string doctorID
    {
        get => doctorId;
        private set => doctorId = value;
    }

    public string Specialization
    {
        get => specialization;
        set => specialization = value;
    }

    public string PhoneNumber
    {
        get; private set;
    }

    public string Gender
    {
        get => gender;
        set => gender = value;
    }

    internal int YearsOfExperience
    {
        get => experience;
        set => experience = value > 0 ? value : 0;
    }

    public double Salary
    {
        get => salary;
        set => salary = value >= 0 ? value : 0;
    }

    public string Availability
    {
        get => availability;
        set => availability = value;
    }

    public Doctor(string fullName, string email, string password, string specialization, string phoneNumber,
                  string gender, int experience, double salary, string availability, string DoctorID)
        : base(fullName, email, password, "doctor")
    {
        doctorID = DoctorID;
        Specialization = specialization;
        PhoneNumber = phoneNumber;
        Gender = gender;
        YearsOfExperience = experience;
        Salary = salary;
        Availability = availability;

        //  Initialize the new properties
        ClinicId = 0;
        DepartmentId = 0;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"[DoctorID: {doctorID}] | {Specialization} | Exp: {YearsOfExperience} yrs | Phone: {PhoneNumber}");
    }
}

