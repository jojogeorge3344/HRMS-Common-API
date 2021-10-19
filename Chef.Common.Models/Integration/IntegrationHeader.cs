using Chef.Common.Core;
using Chef.Finance.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class IntegrationHeader : Model
    {
        public int BusinessPartnerId { get; set; }

        public string BusinessPartnerCode { get; set; }

        public string BusinessPartnerName { get; set; }

        public DateTime DocumentDate { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [StringLength(6)]
        public string TransactionCurrencyCode { get; set; }

        public int ExchangeRateId { get; set; }

        public decimal ExchangeRate { get; set; }

        public DateTime ExchangeDate { get; set; }

        [StringLength(250)]
        public string Narration { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalAmountInBaseCurrency { get; set; }

        [Required]
        public int BranchId { get; set; }

        public ApproveStatus ApproveStatus { get; set; }

        public decimal TaxAmount { get; set; }

        public DocumentType SourceDocumentType { get; set; }

        public string SourceDocumentNumber { get; set; }

        public int SourceDocumentId { get; set; }

        public DateTime SourceDocumentDate { get; set; }

        [Write(false)]
        [Skip(true)]
        public List<IntegrationAccountSummary> integrationAccountSummaries { get; set; }
    }
}
