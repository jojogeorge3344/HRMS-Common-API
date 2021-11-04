using System.Collections.Generic;

namespace Chef.ApprovalSystem.Models
{
    public class WorkflowLevelViewModel
    {
        public string LevelName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int Level { get; set; }
        public bool IsDefault { get; set; }
        public IEnumerable<WorkflowLevels> WorkflowList { get; set; }
    }
}