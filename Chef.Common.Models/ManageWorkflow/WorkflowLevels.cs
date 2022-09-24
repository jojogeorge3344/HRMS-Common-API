using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Common.Models
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

        public string DataType { get; set; }

        [Write(false)]
        [Skip(true)]
        public int Level { get; set; }

        [Write(false)]
        [Skip(true)]
        public string LevelName { get; set; }

        [Write(false)]
        [Skip(true)]
        public int RoleID { get; set; }

        [Write(false)]
        [Skip(true)]
        public string RoleName { get; set; }

        [Write(false)]
        [Skip(true)]
        public bool IsDefault { get; set; }

        public string Operators { get; set; }
    }
}