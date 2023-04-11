using Chef.Common.Core;
using Chef.Common.Core.Logging;

namespace Chef.Common.Models;

public class TaxClassTaxRate : Model, IAuditable
{
    public int TaxClassId { get; set; }
    public int TaxRateId { get; set; }
    public TaxBasedOn BasedOn { get; set; }
}
