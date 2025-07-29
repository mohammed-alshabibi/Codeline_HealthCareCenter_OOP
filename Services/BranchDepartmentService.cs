using System;
using System.Collections.Generic;
using System.Linq;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.Helpers;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class BranchDepartmentService : IBranchDepartmentService
    {
        private List<BranchDepartment> _branchDeps;

        public BranchDepartmentService()
        {
            _branchDeps = BranchDepartmentFileHelper.LoadBranchDepartments();
        }

        public void AddDepartmentToBranch(BranchDepDTO dto)
        {
            int newId = _branchDeps.Count > 0 ? _branchDeps.Max(x => x.BranchDepartmentId) + 1 : 1;

            BranchDepartment bd = new BranchDepartment
            {
                BranchDepartmentId = newId,
                BranchId = dto.BranchId,
                DepartmentId = dto.DepartmentId
            };

            _branchDeps.Add(bd);
            BranchDepartmentFileHelper.SaveBranchDepartments(_branchDeps);
        }
        public void AssignDepartmentToBranch()
        {
            Console.Write("Enter Branch ID: ");
            int branchId = int.Parse(Console.ReadLine());

            Console.Write("Enter Department ID: ");
            int departmentId = int.Parse(Console.ReadLine());

            var dto = new BranchDepDTO
            {
                BranchId = branchId,
                DepartmentId = departmentId
            };

            AddDepartmentToBranch(dto);
            Console.WriteLine(" Department assigned to branch.");
        }

        public IEnumerable<DepartmentDTO> GetDepartmentsByBranch(int bid)
        {
            return _branchDeps
                .Where(bd => bd.BranchId == bid)
                .Select(bd => new DepartmentDTO { DepartmentId = bd.DepartmentId }); // Minimal DTO
        }

        public IEnumerable<Branch> GetBranchsByDepartment(int did)
        {
            return _branchDeps
                .Where(bd => bd.DepartmentId == did)
                .Select(bd => new Branch { BranchId = bd.BranchId }); // Simplified return
        }

        public void UpdateBranchDepartment(BranchDepartment branchDepartment)
        {
            var bd = _branchDeps.FirstOrDefault(b =>
                b.BranchDepartmentId == branchDepartment.BranchDepartmentId);

            if (bd != null)
            {
                bd.BranchId = branchDepartment.BranchId;
                bd.DepartmentId = branchDepartment.DepartmentId;
                BranchDepartmentFileHelper.SaveBranchDepartments(_branchDeps);
            }
        }

        public BranchDepartment GetBranchDep(int departmentId, int branchId)
        {
            return _branchDeps.FirstOrDefault(bd => bd.BranchId == branchId && bd.DepartmentId == departmentId);
        }

        public IEnumerable<DepartmentDTO> GetDepartmentsByBranchName(string branchName)
        {
            // You would normally join with BranchService to get name → id
            return new List<DepartmentDTO>(); // Stub
        }
    }
}
