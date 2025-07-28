using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeline_HealthCareCenter_OOP.Models 
{

          public class Clinic // Represents a clinic in the healthcare system
    {
        public int ClinicId { get; set; }  // Unique identifier for the clinic
        public string ClinicName { get; set; } // Name of the clinic
        public string Department { get; set; } // Name of the department within the clinic
        public string Location { get; set; } // Location of the clinic
    }
            
    }

