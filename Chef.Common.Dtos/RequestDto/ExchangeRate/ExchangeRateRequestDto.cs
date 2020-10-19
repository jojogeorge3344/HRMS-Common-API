using System;

namespace Chef.Common.Dtos
{
    public class ExchangeRateRequestDto
    {  
        public string BaseCurrencyCode { get; set; } 
        public string TransactionCurrencyCode { get; set; } 
        public string TransactionDate { get; set; }  
    }
}
