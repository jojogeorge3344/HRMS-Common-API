using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;
using Chef.Common.Types;

namespace Chef.Common.Models
{
    public class Currency : Model
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        [StringLength(5)]
        public string Symbol { get; set; }

        [Required]
        public Position SymbolPosition { get; set; }      

        [StringLength(15)]
        public string Fraction { get; set; }

        public string DisplayFormat { get; set; }

        public string WordFormat { get; set; }

        public int ExchangeVariationUp { get; set; }

        public int ExchangeVariationDown { get; set; }

        public int CurrencyDenomination { get; set; }

        public bool IsActive { get; set; }
    }
}