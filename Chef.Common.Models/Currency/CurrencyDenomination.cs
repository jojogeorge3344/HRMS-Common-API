using Chef.Common.Core;
using Chef.Common.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class CurrencyDenomination : Model
    {
        [Required]
        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public DenominationType Type { get; set; }
    }
}