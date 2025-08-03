using System;
using System.Collections.Generic;
using System.Linq;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.Helpers;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public class BranchDepartmentService : IBranchDepartmentService // Implementation of IBranchDepartmentService
    {
        private List<BranchDepartment> _branchDeps; // List to store branch-department relationships

        
        public BranchDepartmentService() // Constructor to initialize the service
        {
            _branchDeps = BranchDepartmentFileHelper.LoadBranchDepartments();
        }

        
        public void AddDepartmentToBranch(BranchDepDTO dto) // Method to add a department to a branch
        {
            int newId = _branchDeps.Count > 0 ? _branchDeps.Max(x => x.BranchDepartmentId) + 1 : 1; // Generate new ID based on existing ones

            BranchDepartment bd = new BranchDepartment // Create a new BranchDepartment object
            {
                BranchDepartmentId = newId,
                BranchId = dto.BranchId,
                DepartmentId = dto.DepartmentId
            };

            _branchDeps.Add(bd);
            BranchDepartmentFileHelper.SaveBranchDepartments(_branchDeps);
        }

        // Method to assign a department to a branch via user input
        public void AssignDepartmentToBranch()
        {
            Console.Write("Enter Branch ID: ");
            string? branchInput = Console.ReadLine();
            if (!int.TryParse(branchInput, out int branchId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Branch ID.");
                Console.ResetColor();
                return;
            }

            Console.Write("Enter Department ID: ");
            string? departmentInput = Console.ReadLine();
            if (!int.TryParse(departmentInput, out int departmentId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Department ID.");
                Console.ResetColor();
                return;
            }
            var dto = new BranchDepDTO
            {
                BranchId = branchId,
                DepartmentId = departmentId
            };

            AddDepartmentToBranch(dto);
            Console.WriteLine(" Department assigned to branch.");
        }

        // Method to get departments by branch ID
        public IEnumerable<DepartmentDTO> GetDepartmentsByBranch(int bid)
        {
            return _branchDeps
                .Where(bd => bd.BranchId == bid)
                .Select(bd => new DepartmentDTO { DepartmentId = bd.DepartmentId }); // Minimal DTO
        }

        // Method to get branches by department ID
        public IEnumerable<Branch> GetBranchsByDepartment(int did)
        {
            return _branchDeps
                .Where(bd => bd.DepartmentId == did)
                .Select(bd => new Branch { BranchId = bd.BranchId }); // Simplified return
        }

        // Method to update an existing branch-department relationship
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

        // Method to get a specific branch-department relationship by IDs
        public BranchDepartment? GetBranchDep(int departmentId, int branchId)
        {
            return _branchDeps.FirstOrDefault(bd => bd.BranchId == branchId && bd.DepartmentId == departmentId);
        }

        // Method to get departments by branch name (stubbed for now)
        public IEnumerable<DepartmentDTO> GetDepartmentsByBranchName(string branchName)
        {
            // You would normally join with BranchService to get name to id
            return new List<DepartmentDTO>(); 
        }
    }
}
