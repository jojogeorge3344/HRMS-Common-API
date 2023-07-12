using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Trading.Models;

public class TermsAndConditionsMaster : Model
{
    [Required]
    [Unique(true)]
    public string Code { get; set; }
    [Required]
    [Unique(true)]
    public string Name { get; set; }
    [Required]
    //[StringLength(280)]
    public string TermsAndConditions { get; set; }
}
