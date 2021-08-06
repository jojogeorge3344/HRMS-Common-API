using Chef.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chef.ApprovalSystem.Models
{
    public class WorkflowLevels : Model
    {
        [ForeignKey("workflow")]
        public int WorkflowId { get; set; }

        public int NodeControlFieldId { get; set; }//Foreignkey from admin.nodecontrolfield

        public string NodeControlFieldName { get; set; }

        public string Condition { get; set; }

        public string ConditionName { get; set; }

        public string Value { get; set; }
        public string datatype { get; set; }

        [Write(false)]
        [Skip(true)]
        public string LevelName { get; set; }
        [Write(false)]
        [Skip(true)]
        public int RoleID { get; set; }
        [Write(false)]
        [Skip(true)]
        public string RoleName { get; set; }
    }
}
