using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;
using Chef.Common.Models.Types;

namespace Chef.Common.Models
{
    public class Tax : Model
    {
        [Required]
        [Unique(true)]
        [Code]
        public string Code { get; set; }

        [Required]
        [Unique(true)]
        public string Name { get; set; }

        [Required]
        public TaxType TaxType { get; set; }

        [Required]
        public float TaxPercent { get; set; }

        [ForeignKeyId(typeof(TaxJurisdiction))]
        [Required]
        public int TaxJurisdictionId { get; set; }

        [ForeignKeyCode(typeof(TaxJurisdiction)), Code]
        [Required]
        public string TaxJurisdictionCode { get; set; }

        public string TaxDescription { get; set; }

        public IEnumerable<SubTax> SubTaxes { get; set; }
    }
}
