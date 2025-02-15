﻿namespace Chef.Common.Models;

public class SubModulePermissionView
{
    public int Id { get; set; }
    public int NodeId { get; set; }
    public int PermissionId { get; set; }
    public int NodePermissionId { get; set; }
    public string PermissionName { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int ModuleId { get; set; }
    public string ModuleName { get; set; }
    public string NodeName { get; set; }
}
