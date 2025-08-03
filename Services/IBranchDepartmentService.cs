using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IBranchDepartmentService
    {
        void AddDepartmentToBranch(BranchDepDTO department); // Method to add a department to a branch
        IEnumerable<DepartmentDTO> GetDepartmentsByBranch(int bid); // Method to get departments by branch ID
        IEnumerable<Branch> GetBranchsByDepartment(int did); // Method to get branches by department ID
        void UpdateBranchDepartment(BranchDepartment branchDepartment); // Method to update an existing branch-department relationship
        BranchDepartment? GetBranchDep(int departmentId, int branchId); // Method to get a specific branch-department relationship
        IEnumerable<DepartmentDTO> GetDepartmentsByBranchName(string branchName); // Method to get departments by branch name

    }
}