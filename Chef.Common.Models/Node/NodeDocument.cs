﻿using Chef.Common.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models;

public class NodeDocument : Model
{
    [ForeignKey("Node")]
    public int NodeId { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }
}
