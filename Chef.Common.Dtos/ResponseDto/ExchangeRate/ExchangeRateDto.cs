using System;

namespace Chef.Common.Dtos
{
    public class ExchangeRateDto
    {
        public string BaseCurrencyCode { get; set; }
        public string TransactionCurrencyCode { get; set; }
        public DateTime ExchangeDate { get; set; }
        public float ExchangeRate { get; set; }
        public float InverseExchangeRate => 1 / ExchangeRate;
    }
}
