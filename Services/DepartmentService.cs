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

        public DepartmentService()
        {
            _departments = DepartmentFileHelper.LoadDepartments();
        }

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
        //  Used in case "5"
        public void AddDepartment()
        {
            Console.Write("Enter Department Name: ");
            string name = Console.ReadLine();

            var dto = new DepartmentDTO
            {
                DepartmentName = name
            };

            CreateDepartment(dto);
            Console.WriteLine(" Department added.");
        }

        //  Used in case "8"
        public void ShowDepartments()
        {
            Console.WriteLine("\n Departments List:");
            foreach (var dept in _departments)
            {
                Console.WriteLine($"- ID: {dept.DepartmentId}, Name: {dept.DepartmentName}, Active: {dept.IsActive}");
            }
        }

        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            return _departments.Select(d => new DepartmentDTO { DepartmentName = d.DepartmentName });
        }

        public void UpdateDepartment(DepartmentDTO departmentDto)
        {
            var dept = _departments.FirstOrDefault(d => d.DepartmentName == departmentDto.DepartmentName);
            if (dept != null)
            {
                dept.DepartmentName = departmentDto.DepartmentName;
                DepartmentFileHelper.SaveDepartments(_departments);
            }
        }

        public void SetDepartmentActiveStatus(int departmentId, bool isActive)
        {
            var dept = _departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (dept != null)
            {
                dept.IsActive = isActive;
                DepartmentFileHelper.SaveDepartments(_departments);
            }
        }

        public Department GetDepartmentByName(string department)
        {
            return _departments.FirstOrDefault(d => d.DepartmentName.Equals(department, StringComparison.OrdinalIgnoreCase));
        }

        public DepartmentDTO GetDepartmentByid(int did)
        {
            var dept = _departments.FirstOrDefault(d => d.DepartmentId == did);
            return dept == null ? null : new DepartmentDTO { DepartmentName = dept.DepartmentName };
        }

        public string GetDepartmentName(int depId)
        {
            return _departments.FirstOrDefault(d => d.DepartmentId == depId)?.DepartmentName ?? "Not Found";
        }
    }
}
