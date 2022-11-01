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

        public CurrencyNumberFormatType CurrencyNumberFormatType { get; set; }

        public float ExchangeVariationUp { get; set; }

        public float ExchangeVariationDown { get; set; }

        public bool IsActive { get; set; }
    }
}