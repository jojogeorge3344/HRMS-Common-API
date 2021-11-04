using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Trading.Models
{
    public class PurchaseOffice : Model
    {
        [Required]
        [Unique(true)]
        [StringLength(6)]
        [Code]
        public string PurchaseOfficeCode { get; set; }

        [Required]
        [Unique(true)]
        public string PurchaseOfficeName { get; set; }
    }
}

