using Chef.Common.Core;
using Chef.Common.Types;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class SubTax : Model
    {
        [ForeignKeyId(typeof(TaxOld))]
        [Required]
        [Unique(true), Composite(Index = 1, GroupNumber = 2)]
        public int? TaxId { get; set; } = 0;

        [Required]
        [Unique(true), Composite(Index = 2, GroupNumber = 2)]
        public string SubTaxName { get; set; }

        [Required]
        public float SubTaxPercent { get; set; }

        [Required]
        public TaxBase TaxBase { get; set; }
    }
}
