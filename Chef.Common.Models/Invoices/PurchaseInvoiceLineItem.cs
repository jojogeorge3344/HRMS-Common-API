using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Finance.Models;

public class PurchaseInvoiceLineItem : TransactionModel
{
    [Required]
    [ForeignKey("PurchaseInvoice")]
    public int PurchaseInvoiceId { get; set; }

    [Required]
    public int LineNumber { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public decimal TaxPercentage { get; set; }

    [Required]
    public decimal TaxAmount { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }
}
