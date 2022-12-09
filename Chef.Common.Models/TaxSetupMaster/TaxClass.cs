using Chef.Common.Core;
using Chef.Common.Models.Types;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models;

public class TaxClass : Model
{
    [Required]
    [Unique(true)]
    [Code]
    public string Code { get; set; }

    [Required]
    [Unique(true)]
    public string Description { get; set; }

    [Required]
    public TaxScope Scope { get; set; }
}
