using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IBranchService
    {
        void AddBranch(BranchDTO branchDto); // Method to add a new branch
        IEnumerable<Branch> GetAllBranches(); // Method to get all branches
        BranchDTO GetBranchById(int id); // Method to get a branch by ID
        BranchDTO GetBranchDetails(string? branchName, int? branchId); // Method to get branch details by name or ID
        Branch GetBranchDetailsByBranchName(string branchName); // Method to get branch details by branch name
        string GetBranchName(int branchId); // Method to get branch name by ID
        void SetBranchStatus(int branchId, bool isActive);  // Method to set branch status (active/inactive)
        void UpdateBranch(int branchId, BranchDTO branchDto); // Method to update branch details
    }
}