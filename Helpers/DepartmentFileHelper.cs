using System;
using System.Collections.Generic;
using System.IO;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Helpers
{
    public static class DepartmentFileHelper
    {
        private static readonly string filePath = "departments.txt";

        public static void SaveDepartments(List<Department> departments)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var dep in departments)
                {
                    writer.WriteLine($"{dep.DepartmentId}|{dep.DepartmentName}|{dep.IsActive}");
                }
            }
        }

        public static List<Department> LoadDepartments()
        {
            List<Department> departments = new();

            if (!File.Exists(filePath))
                return departments;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 3)
                {
                    departments.Add(new Department
                    {
                        DepartmentId = int.Parse(parts[0]),
                        DepartmentName = parts[1],
                        IsActive = bool.Parse(parts[2])
                    });
                }
            }

            return departments;
        }
    }
}
