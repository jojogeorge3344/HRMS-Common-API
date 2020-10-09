using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;
using Chef.Common.Types;

namespace Chef.Common.Models
{
    public class CurrencyDenomination : Model
    {
        [Required]
        [ForeignKey("Common.Currency")]
        public int CurrencyId { get; set; }

        public int Value { get; set; }

        public DenominationType Type { get; set; }
    }
}