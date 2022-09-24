using Chef.Common.Core;
using Chef.Common.Models.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class Tax : Model
    {
        [Required]
        [Unique(true)]
        [Code]
        public string TaxCode { get; set; }

        [Required]
        [Unique(true)]
        public string TaxName { get; set; }

        [Required]
        public TaxType TaxType { get; set; }

        [Required]
        public float TaxPercent { get; set; }

        [ForeignKeyId(typeof(TaxJurisdiction))]
        [Required]
        [Unique(true), Composite(Index = 1, GroupNumber = 2)]
        public int TaxJurisdictionId { get; set; }

        [ForeignKeyCode(typeof(TaxJurisdiction)), Code]
        [Required]
        [Composite(Index = 2, GroupNumber = 2)]
        public string TaxJurisdictionCode { get; set; }

        public string TaxDescription { get; set; }
        [Composite(Index = 3, GroupNumber = 2)]

        public int? SegmentId { get; set; } = 0;
        [Composite(Index = 4, GroupNumber = 2)]

        public int? FamilyId { get; set; } = 0;

        public IEnumerable<SubTax> SubTax { get; set; }
    }
}
