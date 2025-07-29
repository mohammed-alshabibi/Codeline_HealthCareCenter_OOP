using System;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Menus;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.Services;

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

    static async Task SuperAdminLogin(IUserService userService, IDoctorService doctorService, IPatientService patientService, IAuthService authService)
    {
        Console.WriteLine("===  SuperAdmin Login ===");

        var input = new UserInputDTO
        {
            Email = Ask("Email:"),
            Password = Ask("Password:")
        };

        var user = userService.AuthenticateUser(input);

        if (user != null && user.Role == "SuperAdmin")
        {
            Console.WriteLine($" Welcome, {user.FullName}");
            SuperAdminMenu.Show();
        }
        else
        {
            Console.WriteLine(" Invalid SuperAdmin credentials.");

            if (user != null)
                await authService.SaveTokenToCookie(user.UserID.ToString());
            else
                await authService.SaveTokenToCookie("unauthorized");

            SuperAdminMenu.Show();
        }
    }



    static async Task PatientLogin(IPatientService patientService, IAuthService authService)
    {
        Console.WriteLine("=== Patient Login ===");

        var input = new PatientInputDTO
        {
            Email = Ask("Email:"),
            Password = Ask("Password:")
        };

        var patient = patientService.AuthenticatePatient(input); //  use object, not class

        if (patient != null)
        {
            Console.WriteLine($" Welcome, {patient.FullName}!");
            await authService.SaveTokenToCookie("patient_login"); // use a static label or generate token
            PatientMenu.Show(patient); //  show only on success
        }
        else
        {
            Console.WriteLine(" Invalid Patient credentials.");
            await authService.SaveTokenToCookie("unauthorized");
        }
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
