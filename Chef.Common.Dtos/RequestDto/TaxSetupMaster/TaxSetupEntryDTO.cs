using Chef.Common.Models;

namespace Chef.Common.Dtos
{
    public class TaxSetupEntryDTO : TaxOld
    {
        public string TaxJurisdiction { get; set; }
        public string ItemSegmentName { get; set; }
        public string ItemFamilyName { get; set; }
        public bool AutoGenerateCode { get; set; } = true;
    }
}
