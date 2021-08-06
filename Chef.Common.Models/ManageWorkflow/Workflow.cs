using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.ApprovalSystem.Models
{
    public class Workflow : Model
    {
        [ForeignKey("document")]
        public int DocumentId { get; set; }

        public int RoleId { get; set; }//Foreignkey from admin.role
        public string RoleName { get; set; }
        public string WorkflowName { get; set; }

    }
}
