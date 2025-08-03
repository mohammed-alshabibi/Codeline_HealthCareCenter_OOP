using System;
using System.Collections.Generic;
using System.Linq;
using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;
using Codeline_HealthCareCenter_OOP.Services;
using Codeline_HealthCareCenter_OOP.Helpers;

namespace Codeline_HealthCareCenter_OOP.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private List<Department> _departments;
        // Load file to initialize the department list
        public DepartmentService() 
        {
            _departments = DepartmentFileHelper.LoadDepartments();
        }
        // Method to create a new department
        public void CreateDepartment(DepartmentDTO departmentDto)
        {
            int newId = _departments.Count > 0 ? _departments.Max(d => d.DepartmentId) + 1 : 1;

            Department newDept = new Department
            {
                DepartmentId = newId,
                DepartmentName = departmentDto.DepartmentName,
                IsActive = true
            };

            _departments.Add(newDept);
            DepartmentFileHelper.SaveDepartments(_departments);
        }
        // Method to display all departments
        public void ShowDepartments()
        {
            Console.WriteLine("\n Departments List:");
            foreach (var dept in _departments)
            {
                Console.WriteLine($"- ID: {dept.DepartmentId}, Name: {dept.DepartmentName}, Active: {dept.IsActive}");
            }
        }
        // Method to get all departments as DTOs
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            return _departments.Select(d => new DepartmentDTO { DepartmentName = d.DepartmentName });
        }
        // Method to get a department by ID
        public void UpdateDepartment(DepartmentDTO departmentDto)
        {
            var dept = _departments.FirstOrDefault(d => d.DepartmentName == departmentDto.DepartmentName);
            if (dept != null)
            {
                dept.DepartmentName = departmentDto.DepartmentName;
                DepartmentFileHelper.SaveDepartments(_departments);
            }
        }
        // Method to set department active status
        public void SetDepartmentActiveStatus(int departmentId, bool isActive) 
        {
            var dept = _departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (dept != null)
            {
                dept.IsActive = isActive;
                DepartmentFileHelper.SaveDepartments(_departments);
            }
        }
        // Method to get a department by name
        public Department? GetDepartmentByName(string department)
        {
            return _departments.FirstOrDefault(d => d.DepartmentName.Equals(department, StringComparison.OrdinalIgnoreCase));
        }
        // Method to get a department by ID and return as DTO
        public DepartmentDTO? GetDepartmentByid(int did)
        {
            var dept = _departments.FirstOrDefault(d => d.DepartmentId == did);
            return dept == null ? null : new DepartmentDTO { DepartmentName = dept.DepartmentName };
        }
        // Method to get a department name by ID
        public string GetDepartmentName(int depId)
        {
            return _departments.FirstOrDefault(d => d.DepartmentId == depId)?.DepartmentName ?? "Not Found";
        }
    }
}
