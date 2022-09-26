using Chef.Common.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Common.Models
{
    public class CurrencyExchangeRate : Model
    {
        [Required]
        [ForeignKey("Common.Currency")]
        public int BaseCurrencyId { get; set; }

        [Required]
        public string BaseCurrencyCode { get; set; }

        [Required]
        [ForeignKey("Common.Currency")]
        public int TransactionCurrencyId { get; set; }

        [Required]
        public string TransactionCurrencyCode { get; set; }

        [Required]
        public DateTime ExchangeDate { get; set; }

        [Required]
        public decimal ExchangeRate { get; set; }
    }
}