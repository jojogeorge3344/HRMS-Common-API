using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Trading.Models
{
    public class Plant : Model
    {
        [Required]
        [Unique(true)]
        public string PlantName { get; set; }

        [Required]
        [Unique(true)]
        [StringLength(6)]
        [Code]
        public string PlantCode { get; set; }

        [Required]
        public string BranchCode { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string CompanyCode { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [ForeignKeyCode(typeof(Location))]
        public string LocationCode { get; set; }

        [ForeignKeyId(typeof(Address))]
        public int? ShipToAddressId { get; set; }

        [ForeignKeyId(typeof(Address))]
        public int? BillToAddressId { get; set; }

        public string PlantManager { get; set; } // Username reference
    }
}
