using Chef.Common.Core;

namespace Chef.Common.Models;

public class TaxRate : Model
{
    [Unique(true)]
    public string Code { get; set; }
    public string Description { get; set; }
    public double Rate { get; set; }

    //Jurisdiction which has multiple locations.
  //  [ForeignKey("taxjurisdiction")]
    public int JurisdictionId { get; set; }
}
