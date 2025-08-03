using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Models
{
    public class BranchDepartment
    {
        public int BranchDepartmentId { get; set; }

        // Foreign Keys
        public int BranchId { get; set; }
        public int DepartmentId { get; set; }
    }

}
