using Chef.Common.Models;

namespace Chef.Common.Dtos
{
    public class TaxJurisdictionEntryDTO : TaxJurisdiction
    {

        public bool AutoGenerateCode { get; set; } = true;
        //public string Host { get; set; }
    }
}
