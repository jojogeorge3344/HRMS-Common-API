namespace Chef.Common.Authentication.Models;

public class Permission : Model
{
    public ModuleType Module { get; set; }
    public string Controller { get; set; }
    public string Action { get; set; }
    public string GroupCode { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
}
