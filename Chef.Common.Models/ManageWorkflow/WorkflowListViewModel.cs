﻿namespace Chef.Common.Models;

public class WorkflowListViewModel
{
    public int WorkflowId { get; set; }
    public int DocumentId { get; set; }
    public string DocumentName { get; set; }
    public string WorkflowName { get; set; }
    public int RoleId { get; set; }
    public string ApproverRole { get; set; }
    public int SubmoduleId { get; set; }
    public int NodeSubmoduleId { get; set; }
    public int NodeDocumentId { get; set; }
    public int ModuleId { get; set; }
    public int Level { get; set; }
    public bool IsActive { get; set; }
}
