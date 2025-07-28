using Codeline_HealthCareCenter_OOP.DTO_s;
using Codeline_HealthCareCenter_OOP.Models;

namespace Codeline_HealthCareCenter_OOP.Services
{
    public interface IDepartmentService
    {
        void CreateDepartment(DepartmentDTO departmentDto);
        IEnumerable<DepartmentDTO> GetAllDepartments();
        void UpdateDepartment(DepartmentDTO departmentDto); // Only one declaration for UpdateDepartment
        void SetDepartmentActiveStatus(int departmentId, bool isActive);
        Department GetDepartmentByName(string department);
        DepartmentDTO GetDepartmentByid(int did);
        string GetDepartmentName(int depId);
    }
}
