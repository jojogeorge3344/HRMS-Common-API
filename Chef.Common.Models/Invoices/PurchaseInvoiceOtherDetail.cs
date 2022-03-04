using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chef.Common.Core;
using Chef.Common.Models;

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