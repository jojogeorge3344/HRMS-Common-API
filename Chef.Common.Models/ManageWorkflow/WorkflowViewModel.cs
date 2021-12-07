using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.Common.Models
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
