using System;
using System.Collections.Generic;
using System.IO;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Helpers
{
    public static class BranchDepartmentFileHelper
    {
        private static readonly string filePath = "branchdepartments.txt";

        public static void SaveBranchDepartments(List<BranchDepartment> branchDeps)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var bd in branchDeps)
                {
                    writer.WriteLine($"{bd.BranchDepartmentId}|{bd.BranchId}|{bd.DepartmentId}");
                }
            }
        }

        public static List<BranchDepartment> LoadBranchDepartments()
        {
            List<BranchDepartment> branchDeps = new();

            if (!File.Exists(filePath))
                return branchDeps;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 3)
                {
                    branchDeps.Add(new BranchDepartment
                    {
                        BranchDepartmentId = int.Parse(parts[0]),
                        BranchId = int.Parse(parts[1]),
                        DepartmentId = int.Parse(parts[2])
                    });
                }
            }

            return branchDeps;
        }
    }
}
