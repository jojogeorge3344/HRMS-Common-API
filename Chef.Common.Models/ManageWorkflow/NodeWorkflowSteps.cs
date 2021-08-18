using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.ApprovalSystem.Models
{
    public class NodeWorkflowSteps
    {
        public int nodedocumentid {get;set;}
        public int workflowid { get;set;}
        public int roleid { get; set; }
        public string rolename {get;set;}
        public string workflowname { get;set;}
        public string nodecontrolfieldname { get;set;}
        public string conditionname { get; set; }
        public string value { get; set; }
        public string datatype { get; set; }
        public string operators { get; set; }
    }
}
