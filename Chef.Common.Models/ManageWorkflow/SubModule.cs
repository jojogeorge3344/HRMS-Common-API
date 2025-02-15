﻿using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models;

public class SubModule : Model
{
    [Required]
    public string SubModuleName { get; set; }

    public bool IsEnabled { get; set; }

    public int ModuleID { get; set; }

    public int NodeSubModuleID { get; set; }

    public int WorkflowDocumentCount { get; set; }

    [Write(false)]
    [Skip(true)]
    [SqlKata.Ignore]
    public int TotalDocumentCount { get; set; }
}
