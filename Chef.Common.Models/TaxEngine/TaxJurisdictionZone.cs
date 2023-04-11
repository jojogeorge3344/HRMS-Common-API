using Chef.Common.Core;

namespace Chef.Common.Models;

public class TaxJurisdictionZone : Model
{
    public int TaxJurisdictionId { get; set; }

    public int CountryId { get; set; }
    public string CountryCode { get; set; }

    public int StateId { get; set; }
    public string StateCode { get; set; }
}
