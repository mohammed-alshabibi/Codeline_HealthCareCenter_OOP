using System;
using System.Collections.Generic;
using System.Linq;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Helpers;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.Services;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class BranchService : IBranchService
    {
        private List<Branch> _branches;

        public BranchService()
        {
            _branches = BranchFileHelper.LoadBranches();
        }

        public void AddBranch(BranchDTO branchDto)
        {
            int newId = _branches.Count > 0 ? _branches.Max(b => b.BranchId) + 1 : 1;

            Branch newBranch = new Branch
            {
                BranchId = newId,
                BranchName = branchDto.BranchName,
                Location = branchDto.Location,
                IsActive = true
            };

            _branches.Add(newBranch);
            BranchFileHelper.SaveBranches(_branches);
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            return _branches;
        }

        public BranchDTO GetBranchById(int id)
        {
            var branch = _branches.FirstOrDefault(b => b.BranchId == id);
            return branch == null ? null : new BranchDTO
            {
                BranchName = branch.BranchName,
                Location = branch.Location
            };
        }

        public BranchDTO GetBranchDetails(string? branchName, int? branchId)
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

        public Branch GetBranchDetailsByBranchName(string branchName)
        {
            return _branches.FirstOrDefault(b => b.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase));
        }

        public string GetBranchName(int branchId)
        {
            return _branches.FirstOrDefault(b => b.BranchId == branchId)?.BranchName ?? "Not found";
        }

        public void SetBranchStatus(int branchId, bool isActive)
        {
            var branch = _branches.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                branch.IsActive = isActive;
                BranchFileHelper.SaveBranches(_branches);
            }
        }

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
