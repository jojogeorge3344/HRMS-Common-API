namespace Chef.Common.Models;

public class NodeWorkflowSteps
{
    public int NodeDocumentId { get; set; }
    public int WorkFlowId { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string WorkFlowName { get; set; }
    public string NodeControlFieldName { get; set; }
    public string ConditionName { get; set; }
    public string Value { get; set; }
    public string Datatype { get; set; }
    public string Operators { get; set; }
    public bool IsDefault { get; set; }
    public int Level { get; set; }
}
