namespace Chef.Common.Authentication.Models;

public class PermissionGroup : Model
{
    public ModuleType Module { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
}
