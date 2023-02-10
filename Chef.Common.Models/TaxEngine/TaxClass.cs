using Chef.Common.Core;
using Chef.Common.Core.Logging;
using Chef.Common.Models.Types;
using System.Collections.Generic;

namespace Chef.Common.Models;

public class TaxClass : Model, IAuditable
{
    [Unique(true)]
    public string Code { get; set; }
    public string Description { get; set; }
    public TaxScope Scope { get; set; }

    [SqlKata.Ignore]
    [Write(false)]
    [Skip(true)]
    public IEnumerable<TaxClassTaxRate> TaxClassTaxRates { get; set; }
}
