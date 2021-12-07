using Chef.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
{
    public class ManageWorkflow
    {
        [Required]
        public int DocumentId { get; set; }

        [Required]
        public int WorkflowId { get; set; }

        [Required]
        public string WorkflowName { get; set; }

        [Required]
        public int RoleId { get; set; }
        public int NodeId { get; set; }
        public int Level { get; set; }

        [Required]
        public string RoleName { get; set; }
        public bool IsDefault { get; set; }


        public IEnumerable<WorkflowLevels> WorkflowLevelsList { get; set; }       
        
    }
}
