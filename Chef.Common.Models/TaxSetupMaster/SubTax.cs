using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class SubTax : Model
    {
        public int TaxId { get; set; }

        public string SubTaxName { get; set; }

        public int SubTaxPercentage { get; set; }

        public string TaxBase { get; set; }
    }
}
