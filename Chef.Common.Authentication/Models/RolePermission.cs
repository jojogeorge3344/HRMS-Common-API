namespace Chef.Common.Authentication.Models;

public class RolePermission : Model
{
    public string Role { get; set; }
    public int PermissionId { get; set; }
}
