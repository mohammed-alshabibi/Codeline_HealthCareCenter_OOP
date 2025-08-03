using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IDepartmentService
    {
        void CreateDepartment(DepartmentDTO departmentDto); // Method to create a new department
        IEnumerable<DepartmentDTO> GetAllDepartments(); // Method to retrieve all departments
        void UpdateDepartment(DepartmentDTO departmentDto); // Method to update an existing department
        void SetDepartmentActiveStatus(int departmentId, bool isActive); // Method to set the active status of a department
        Department GetDepartmentByName(string department); // Method to get a department by name
        DepartmentDTO GetDepartmentByid(int did); // Method to get a department by ID
        string GetDepartmentName(int depId); // Method to get a department name by ID
    }
}
