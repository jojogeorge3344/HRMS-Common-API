using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;
using Chef.Common.Types;

namespace Chef.Common.Models
{
    public class IntegrationHeader : Model
    {
        public int BusinessPartnerId { get; set; } = 0;

        public string BusinessPartnerCode { get; set; }

        public string BusinessPartnerName { get; set; }

        public DateTime DocumentDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime TransactionDate { get; set; }=DateTime.UtcNow;

        [StringLength(6)]
        public string TransactionCurrencyCode { get; set; }

        public int ExchangeRateId { get; set; } = 0;

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

        public DateTime SourceDocumentDate { get; set; } = DateTime.UtcNow;

        public TransactionType TransactionType { get; set; }

        public TransactionOrgin TransactionOrgin { get; set; }

        public int InvoiceId { get; set; }

        public String InvoiceNumber { get; set; }

        public decimal InvoiceAmount { get; set; }

        [Write(false)]
        [Skip(true)]
        public List<IntegrationAccountSummary> integrationAccountSummaries { get; set; }

        [Write(false)]
        [Skip(true)]
        public List<IntegrationPaymentTerm> integrationPaymentTerms { get; set; }
    }
}
