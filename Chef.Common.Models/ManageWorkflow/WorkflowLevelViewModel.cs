using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.ApprovalSystem.Models
{
    public class WorkflowLevelViewModel
    {
        public string LevelName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<WorkflowLevels> WorkflowList { get; set; }
    }
}
