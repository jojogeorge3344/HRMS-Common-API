using Chef.Common.Core;
using System.Collections.Generic;

namespace Chef.Common.Models;

public class TaxJurisdiction : Model
{
    [Unique(true)]
    public string Code { get; set; }
    public string Description { get; set; }

    [SqlKata.Ignore]
    [Write(false)]
    [Skip(true)]
    public IEnumerable<TaxJurisdictionZone> TaxJurisdictionZones { get; set; }
}
