using System.Collections.Generic;

namespace Chef.ApprovalSystem.Models
{
    public class WorkflowViewModel
    {
        public string DocumentName { get; set; }
        public int ModuleID { get; set; }
        public int SubModuleID { get; set; }
        public int NodeSubModuleID { get; set; }
        public int NodeDocumentID { get; set; }
        public IEnumerable<WorkflowListViewModel> WorkflowList { get; set; }
    }
}