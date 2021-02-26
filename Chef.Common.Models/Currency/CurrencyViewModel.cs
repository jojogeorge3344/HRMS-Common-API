using Chef.Common.Core;
using Chef.Common.Types;

namespace Chef.Common.Models
{
    public class CurrencyViewModel : Model
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Symbol { get; set; }

        public Position SymbolPosition { get; set; }

        public string Fraction { get; set; }

        public string DisplayFormat { get; set; }

        public CurrencyNumberFormatType CurrencyNumberFormatType { get; set; }

        public float ExchangeVariationUp { get; set; }

        public float ExchangeVariationDown { get; set; }

        public bool IsActive { get; set; }

        public int CurrencyId { get; set; }

        public int Value { get; set; }

        public DenominationType Type { get; set; }
    }
}