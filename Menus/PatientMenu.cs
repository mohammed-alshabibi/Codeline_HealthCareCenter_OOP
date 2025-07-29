using System;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Implementations;
using Codeline_HealthCareCenter_OOP.Services;

namespace Codeline_HealthCareCenter_OOP.Menus
{
    public static class PatientMenu
    {
        public static void Show()
        {
            PatientService patientService = new PatientService();
            BookingService bookingService = new BookingService();
            PatientRecordService recordService = new PatientRecordService();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🏥 Welcome to Patient Portal");
                Console.ResetColor();

                Console.WriteLine("1. 📝 Signup");
                Console.WriteLine("2. 🔐 Login");
                Console.WriteLine("3. ❌ Exit");

                Console.Write("\nChoose an option: ");
                string mainChoice = Console.ReadLine();

                if (mainChoice == "1")
                {
                    var input = new PatientInputDTO();

                    Console.Write("Full Name: ");
                    input.FullName = Console.ReadLine();

                    Console.Write("Email: ");
                    input.Email = Console.ReadLine();

                    Console.Write("Password: ");
                    input.Password = Console.ReadLine();

                    Console.Write("Phone Number: ");
                    input.PhoneNumber = Console.ReadLine();

                    patientService.AddPatient(input);
                    Console.WriteLine("✅ Signup successful. You may now login.");
                    Pause();
                }
                else if (mainChoice == "2")
                {
                    Console.Write("Email: ");
                    string email = Console.ReadLine();

                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    var patient = patientService.Login(email, password);

                    if (patient == null)
                    {
                        Console.WriteLine("❌ Invalid email or password.");
                        Pause();
                        continue;
                    }

                    // ✅ Logged in successfully
                    while (true)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"👤 Welcome {patient.FullName} - Patient Menu");
                        Console.ResetColor();

                        Console.WriteLine("1. 📋 View Medical Records");
                        Console.WriteLine("2. 🕒 Book Appointment");
                        Console.WriteLine("3. 📅 View Appointments");
                        Console.WriteLine("4. ❌ Logout");

                        Console.Write("\nChoose an option: ");
                        string innerChoice = Console.ReadLine();

                        if (innerChoice == "1")
                        {
                            Console.Clear();
                            var records = recordService.GetRecords(patient.PatientId);
                            Console.WriteLine("📂 Medical Records:");
                            foreach (var r in records)
                                Console.WriteLine($"🩺 {r.Date} - Diagnosis: {r.Inspection}, Treatment: {r.Treatment}");
                            Pause();
                        }
                        else if (innerChoice == "2")
                        {
                            Console.Clear();
                            Console.WriteLine("🕒 Book Appointment");

                            Console.Write("Clinic ID: ");
                            int clinicId = int.Parse(Console.ReadLine());

                            Console.Write("Department ID: ");
                            int departmentId = int.Parse(Console.ReadLine());

                            Console.Write("Date (yyyy-MM-dd): ");
                            DateTime date = DateTime.Parse(Console.ReadLine());

                            var booking = new BookingInputDTO
                            {
                                ClinicId = clinicId,
                                DepartmentId = departmentId,
                                Date = date
                            };

                            bookingService.BookAppointment(booking, patient.PatientId);
                            Console.WriteLine("✅ Appointment booked.");
                            Pause();
                        }
                        else if (innerChoice == "3")
                        {
                            Console.Clear();
                            var appointments = bookingService.GetBookedAppointments(patient.PatientId);
                            Console.WriteLine("📅 Your Appointments:");
                            foreach (var a in appointments)
                                Console.WriteLine($"📆 {a.Date} - Clinic ID: {a.ClinicId}, Dept ID: {a.DepartmentId}");
                            Pause();
                        }
                        else if (innerChoice == "4")
                        {
                            Console.WriteLine("👋 Logged out.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("❌ Invalid option.");
                            Pause();
                        }
                    }
                }
                else if (mainChoice == "3")
                {
                    Console.WriteLine("👋 Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("❌ Invalid option.");
                    Pause();
                }
            }
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
