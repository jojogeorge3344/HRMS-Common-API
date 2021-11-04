using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Finance.Models
{
    public class PurchaseInvoicePaymentTermInstallment : Model
    {
        [Required]
        [ForeignKey("PurchaseInvoice")]
        public int PurchaseInvoiceId { get; set; }

        [Required]
        [ForeignKey("PurchaseInvoicePaymentTerm")]
        public int PurchaseInvoicePaymentTermId { get; set; }

        [Required]
        public int LineNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal BalanceAmount { get; set; }

        public decimal AdjustedAmount { get; set; }

        public decimal AmountInBaseCurrency { get; set; }

        public decimal BalanceAmountInBaseCurrency { get; set; }

        public decimal AdjustedAmountInBaseCurrency { get; set; }        

        public int? CreditPeriodInDays { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal EarlyPaymentDiscountPercentage { get; set; }

        [Required]
        public decimal LatePaymentPenaltyPercentage { get; set; }

        public bool IsRetentionApplicable { get; set; }
    }
}