using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Trading.Models
{
    public class Location : Model
    {
        [Required]
        [Unique(true)]
        [StringLength(6)]
        [Code]
        public string LocationCode { get; set; }

        [Required]
        [Unique(true)]
        public string LocationName { get; set; }

        [ForeignKeyId(typeof(Address))]
        public int ShipToAddressId { get; set; }
    }
}
