namespace Chef.Common.Models;

public class NodePermissionViewModel
{
    public int Id { get; set; }
    public int NodeId { get; set; }
    public int PermissionId { get; set; }
    public string PermissionCode { get; set; }
    public string PermissionName { get; set; }
}
