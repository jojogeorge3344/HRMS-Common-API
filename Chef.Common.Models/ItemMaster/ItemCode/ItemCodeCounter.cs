﻿using Chef.Common.Core;
using Chef.Common.Types;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models;

public class ItemCodeCounter : Model
{
    [Required]
    public ItemCodeType ItemCodeType { get; set; }

    public int? CodeReferenceId { get; set; }

    [Required]
    public int Counter { get; set; }

    public int DeleteCounter { get; set; }
}
