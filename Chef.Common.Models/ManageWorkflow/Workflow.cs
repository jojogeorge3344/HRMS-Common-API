using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.ApprovalSystem.Models
{
    public class Workflow : Model
    {
        [ForeignKey("document")]
        public int DocumentId { get; set; }

        public int RoleId { get; set; }//Foreignkey from admin.role
        public string RoleName { get; set; }
        public string WorkflowName { get; set; }
        public int NodeId { get; set; }
        public int Level { get; set; }
        public bool IsDefault { get; set; }
    }
}
