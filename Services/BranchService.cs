using System;
using System.Collections.Generic;
using System.Linq;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.Services;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class BranchService : IBranchService // Interface implementation
    {
        private List<Branch> _branches;

        public BranchService()
        {
            _branches = BranchFileHelper.LoadBranches();
        }
        // Method to add a new branch
        public void AddBranch(BranchDTO branchDto)
        {

            int newId = _branches.Count > 0 ? _branches.Max(b => b.BranchId) + 1 : 1; // Generate new ID based on existing branches

            Branch newBranch = new Branch
            {
                BranchId = newId,
                BranchName = branchDto.BranchName,
                Location = branchDto.Location,
                IsActive = true
            };

            _branches.Add(newBranch);
            BranchFileHelper.SaveBranches(_branches);
            Console.WriteLine("Branch created successfully.");
        }
        // Method to get all branches
        public IEnumerable<Branch> GetAllBranches()
        {
            return _branches;
        }

        // Method to get a branch by ID
        public BranchDTO? GetBranchById(int id)
        {
            var branch = _branches.FirstOrDefault(b => b.BranchId == id); // Find branch by ID
            return branch == null ? null : new BranchDTO // Return DTO if found
            {
                BranchName = branch.BranchName,
                Location = branch.Location
            };
        }

        // Method to get branch details by name or ID
        public BranchDTO? GetBranchDetails(string? branchName, int? branchId)
        {
            var branch = _branches.FirstOrDefault(b =>
                (!string.IsNullOrEmpty(branchName) && b.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase))
                || (branchId.HasValue && b.BranchId == branchId.Value));

            return branch == null ? null : new BranchDTO
            {
                BranchName = branch.BranchName,
                Location = branch.Location
            };
        }
        // Method to get branch details by branch name
        public Branch? GetBranchDetailsByBranchName(string branchName)
        {
            return _branches.FirstOrDefault(b => b.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase));
        }
        // Method to get branch name by ID
        public string GetBranchName(int branchId)
        {
            return _branches.FirstOrDefault(b => b.BranchId == branchId)?.BranchName ?? "Not found";
        }
        // Method to set branch status (active/inactive)
        public void SetBranchStatus(int branchId, bool isActive)
        {
            var branch = _branches.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                branch.IsActive = isActive;
                BranchFileHelper.SaveBranches(_branches);
            }
        }
        // Method to display all branches
        public void ShowAllBranches()
        {
            Console.WriteLine("\n Branch List:");
            foreach (var branch in _branches)
            {
                Console.WriteLine($"- ID: {branch.BranchId}, Name: {branch.BranchName}, Location: {branch.Location}, Active: {branch.IsActive}");
            }
        }
        // Method to update an existing branch
        public void UpdateBranch(int branchId, BranchDTO branchDto)
        {
            var branch = _branches.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                branch.BranchName = branchDto.BranchName;
                branch.Location = branchDto.Location;
                BranchFileHelper.SaveBranches(_branches);
            }
        }
    }
}
