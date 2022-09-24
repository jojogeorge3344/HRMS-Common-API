using System;
using System.ComponentModel.DataAnnotations;
using Chef.Common.Core;

namespace Chef.Common.Models
{
    public class IntegrationPaymentTermInstallment : Model
    {
        [Required]
        public int LineNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public int? CreditPeriodInDays { get; set; }

        public decimal AmountBaseCurrency { get; set; }

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