using Chef.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Chef.Common.Models
{
    public class BusinessSites : Model
    {
        [Required]
        public int BusinessPartnerId { get; set; }

        [Required]
        [StringLength(16)]
        public string BusinessPartnerCode { get; set; }

        [Required]
        [StringLength(126)]
        public string BusinessPartnerName { get; set; }
    }
}
