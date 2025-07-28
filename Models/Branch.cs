using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public List<BranchDepartment> BranchDepartments { get; set; } = new();
    }

}
