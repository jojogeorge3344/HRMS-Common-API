using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models;

public class ItemSegment : Model
{
    [Required(AllowEmptyStrings = true)]
    [Unique(true)]
    public string ItemSegmentCode { get; set; } = string.Empty;

    [Required]
    [Unique(true)]
    public string ItemSegmentName { get; set; }
}
