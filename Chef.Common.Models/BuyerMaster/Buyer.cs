using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Trading.Models
{
    public class Buyer : Model
    {
        [Required]
        [Unique(true)]
        [StringLength(6)]
        [Code]
        public string BuyerCode { get; set; }
        [Required]
        public string BuyerName { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public decimal BuyerLimit { get; set; }
        //[Required]
        [ForeignKeyId(typeof(PurchaseOffice))]
        public int? PurchaseOfficeId { get; set; } = null;
        // [Required]
        //[ForeignKeyCode(typeof(PurchaseOffice))]
        public string PurchaseOfficeCode { get; set; } 

    }
}
