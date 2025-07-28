using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IBranchDepartmentService
    {
        void AddDepartmentToBranch(BranchDepDTO department);
        IEnumerable<DepDTO> GetDepartmentsByBranch(int bid);
        IEnumerable<Branch> GetBranchsByDepartment(int did);
        void UpdateBranchDepartment(BranchDepartment branchDepartment);
        BranchDepartment GetBranchDep(int departmentId, int branchId);
        IEnumerable<DepDTO> GetDepartmentsByBranchName(string branchName);

    }
}