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


    static string Ask(string label)
    {
        Console.Write($"{label}: ");
        return Console.ReadLine();
    }

}
