using System;
using System.Collections.Generic;
using System.IO;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Helpers
{
    public static class BranchFileHelper
    {
        private static readonly string filePath = "branches.txt";

        //  Save all branches to file
        public static void SaveBranches(List<Branch> branches)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var branch in branches)
                {
                    string line = $"{branch.BranchId}|{branch.BranchName}|{branch.Location}|{branch.IsActive}";
                    writer.WriteLine(line);
                }
            }
        }

        //  Load all branches from file
        public static List<Branch> LoadBranches()
        {
            List<Branch> branches = new List<Branch>();

            if (!File.Exists(filePath))
                return branches;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 4)
                {
                    branches.Add(new Branch
                    {
                        BranchId = int.Parse(parts[0]),
                        BranchName = parts[1],
                        Location = parts[2],
                        IsActive = bool.Parse(parts[3])
                    });
                }
            }

            return branches;
        }
    }
}
