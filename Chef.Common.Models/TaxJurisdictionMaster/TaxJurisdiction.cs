using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class TaxJurisdiction : Model
    {
        [Required(AllowEmptyStrings = true)]
        [Unique(true)]
        [Code]
        public string TaxJurisdictionCode { get; set; } = string.Empty;
        
        [Required]
        [Unique(true)]
        public string TaxName { get; set; }
    }
}
