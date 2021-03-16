using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Trading.Models
{
    public class PurchaseOrderGroup : Model
    {
        [Required]
        [Unique(true)]
        [StringLength(6)]
        [Code]
        public string PurchaseOrderGroupCode { get; set; }
        [Required]
        [Unique(true)]
        public string PurchaseOrderGroupName { get; set; }
    }
}
