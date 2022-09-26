using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chef.Finance.Models
{
    public class PurchaseInvoiceAttachment : Model
    {
        [Required]
        [ForeignKey("PurchaseInvoiceOtherDetail")]
        public int PurchaseInvoiceId { get; set; }

        public string FileName { get; set; }

        public int FileId { get; set; }
    }
}
