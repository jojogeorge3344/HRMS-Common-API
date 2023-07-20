using Chef.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models;

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
    [SqlKata.Ignore]
    public int Level { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public string LevelName { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public int RoleID { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public string RoleName { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public bool IsDefault { get; set; }

    public string Operators { get; set; }
}