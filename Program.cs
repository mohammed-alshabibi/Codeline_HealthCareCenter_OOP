using System;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.Services;
using HospitalSystem.Services;

class Program
{
    static async Task Main(string[] args)
    {
        await StartSystem();
    }
    static async Task StartSystem()
    {
        IUserService userService = new UserService();
        IDoctorService doctorService = new DoctorService();
        IPatientService patientService = new PatientService();
        IAuthService authService = new AuthService();

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n=== Codeline HealthCare System ===");
            Console.WriteLine("1. Login SuperAdmin");
            Console.WriteLine("0. Exit");

            string choice = Ask("Choose");

            switch (choice)
            {
                case "1":
                    SuperAdminLogin(userService, doctorService, patientService, authService);
                    break;
                case "0":
                    Console.WriteLine(" Exiting system. Goodbye!");
                    exit = true;
                    break;

                default:
                    Console.WriteLine(" Invalid option.");
                    break;
            }
        }
    }

    static void ShowAdminMenu(IUserService userService, IPatientService patientService)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n=== Admin Panel ===");
            Console.WriteLine("1. Add Patient");
            Console.WriteLine("2. View All Doctors");
            Console.WriteLine("0. Logout");

            string choice = Ask("Choose");

            switch (choice)
            {
                case "1":
                    AddPatientFromAdmin(patientService);
                    break;

                case "2":
                    var doctors = userService.GetUserByRole("Doctor");
                    Console.WriteLine("=== Doctors List ===");
                    foreach (var doc in doctors)
                        Console.WriteLine($"{doc.FullName} | {doc.Email} | {doc.Role}");
                    break;

                case "0":
                    Console.WriteLine(" Logged out.");
                    exit = true;
                    break;

                default:
                    Console.WriteLine(" Invalid option.");
                    break;
            }
        }
    }
    static void ShowPatientMenu(User patient)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine($"\n=== Patient Menu ({patient.FullName}) ===");
            Console.WriteLine("1. View My Info");
            Console.WriteLine("0. Logout");

            string choice = Ask("Choose");

            switch (choice)
            {
                case "1":
                    Console.WriteLine($"Name: {patient.FullName}");
                    Console.WriteLine($"Email: {patient.Email}");
                    Console.WriteLine($"Role: {patient.Role}");
                    break;

                case "0":
                    Console.WriteLine(" Logged out.");
                    exit = true;
                    break;

                default:
                    Console.WriteLine(" Invalid option.");
                    break;
            }
        }
    }

    static async Task SuperAdminLogin(IUserService userService, IDoctorService doctorService, IPatientService patientService, IAuthService authService)

    {
        Console.WriteLine("=== SuperAdmin Login ===");
        string email = Ask("Email");
        string password = Ask("Password");

        var user = userService.AuthenticateUser(email, password);
        if (user != null && user.Role == "SuperAdmin")
        {
            Console.WriteLine($" Welcome, {user.FullName}");
            ShowSuperAdminMenu(userService, doctorService, patientService);
        }
        else
        {
            Console.WriteLine("Invalid SuperAdmin credentials.");
        }
        await authService.SaveTokenToCookie(user.UserID.ToString());
        ShowSuperAdminMenu(userService, doctorService, patientService);

    }


    static async Task AdminLogin(IUserService userService, IPatientService patientService, IAuthService authService)

    {
        Console.WriteLine("=== Admin Login ===");

        string email = Ask("Email");
        string password = Ask("Password");

        var admin = userService.AuthenticateUser(email, password);

        if (admin != null && admin.Role == "Admin")
        {
            Console.WriteLine($" Welcome, {admin.FullName}");
            ShowAdminMenu(userService, patientService);
        }
        else
        {
            Console.WriteLine("Invalid Admin credentials.");
        }
        await authService.SaveTokenToCookie(admin.UserID.ToString());
        ShowAdminMenu(userService, patientService);

    }

    static async Task PatientLogin(IUserService userService, IAuthService authService)
    {
        Console.WriteLine("=== Patient Login ===");

        string email = Ask("Email");
        string password = Ask("Password");

        var patient = userService.AuthenticateUser(email, password);

        if (patient != null && patient.Role == "Patient")
        {
            Console.WriteLine($" Welcome, {patient.FullName}!");
            ShowPatientMenu(patient);
        }
        else
        {
            Console.WriteLine(" Invalid Patient credentials.");
        }
        await authService.SaveTokenToCookie(patient.UserID.ToString());
        ShowPatientMenu(patient);

    }


    static void AddDoctor(IUserService userService)
    {
        Console.WriteLine("=== Add Doctor ===");

        string name = Ask("Full Name");
        string email = Ask("Email");
        string password = Ask("Password");
        string specialization = Ask("Specialization");
        string phone = Ask("Phone Number");
        string gender = Ask("Gender");

        int experience;
        while (!int.TryParse(Ask("Years of Experience"), out experience))
            Console.WriteLine(" Enter a valid number.");

        double salary;
        while (!double.TryParse(Ask("Salary"), out salary))
            Console.WriteLine(" Enter a valid amount.");

        string availability = Ask("Availability");

        var doctor = new Doctor(name, email, password, specialization, phone, gender, experience, salary, availability);
        userService.AddUser(doctor);

        Console.WriteLine(" Doctor created successfully.");
    }
    static void PatientSelfSignup(IPatientService patientService)
    {
        Console.WriteLine("=== Patient Self Signup ===");

        PatientInputDTO input = new()
        {
            FullName = Ask("Full Name"),
            Email = Ask("Email"),
            Password = Ask("Password"),
            PhoneNumber = Ask("Phone Number"),
            Gender = Ask("Gender"),
            Age = int.Parse(Ask("Age")),
            NationalID = Ask("National ID")
        };

        patientService.AddPatient(input);
        Console.WriteLine("Patient signed up successfully.");
    }

    static void AddPatientFromAdmin(IPatientService patientService)
    {
        Console.WriteLine("=== Add Patient (By Admin) ===");

        PatientInputDTO input = new()
        {
            FullName = Ask("Full Name"),
            Email = Ask("Email"),
            Password = Ask("Password"),
            PhoneNumber = Ask("Phone Number"),
            Gender = Ask("Gender"),
            Age = int.Parse(Ask("Age")),
            NationalID = Ask("National ID")
        };

        patientService.AddPatient(input);
        Console.WriteLine(" Patient added by admin successfully.");
    }


    static string Ask(string label)
    {
        Console.Write($"{label}: ");
        return Console.ReadLine();
    }

}
