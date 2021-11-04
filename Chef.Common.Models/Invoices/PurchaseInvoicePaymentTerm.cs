using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Finance.Models
{
    public class PurchaseInvoicePaymentTerm : Model
    {
        [Required]
        [ForeignKey("PurchaseInvoice")]
        public int PurchaseInvoiceId { get; set; }

        public bool IsRetentionApplicable { get; set; }

        public decimal RetentionPercentage { get; set; }

        public decimal RetentionLockPeriod { get; set; }

        [Required]
        public int NumberOfInstallments { get; set; }

        public int CreditPeriod { get; set; }

        [Write(false)]
        [Skip(true)]
        public List<PurchaseInvoicePaymentTermInstallment> Installments { get; set; }
    }
}