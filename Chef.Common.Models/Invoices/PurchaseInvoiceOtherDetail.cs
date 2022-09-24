using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;

namespace Chef.Finance.Models
{
    public class PurchaseInvoiceOtherDetail : Model
    {
        [Required]
        [ForeignKey("PurchaseInvoice")]
        public int PurchaseInvoiceId { get; set; }

        public string Narration { get; set; }

        [Required]
        public bool IsAttachments { get; set; }
    }
}